import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import {
  Box, Card, CardContent, Typography, CircularProgress, Button, Grid, Stepper, Step, StepLabel, MenuItem, TextField
} from '@mui/material';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import api from '../../services/api';
import { useForm, Controller } from 'react-hook-form';
import AssignUnidadDialog from './AssignUnidadDialog';

const ServicioDetallePage = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [servicio, setServicio] = useState<any>(null);
  const [loading, setLoading] = useState(true);
  const [openAssignDialog, setOpenAssignDialog] = useState(false);
  
  // Para la actualización de estado
  const { control, handleSubmit, reset } = useForm({
    defaultValues: { nuevoEstado: 1, observacion: '' }
  });

  const fetchServicio = async () => {
    setLoading(true);
    try {
      const response = await api.get(`/servicios/${id}`);
      setServicio(response.data);
    } catch (error) {
      console.error('Error fetching servicio:', error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchServicio();
  }, [id]);

  const onUpdateState = async (data: any) => {
    try {
      await api.post(`/servicios/${id}/estado`, {
        estId: data.nuevoEstado,
        observacion: data.observacion
      });
      fetchServicio();
      reset({ nuevoEstado: data.nuevoEstado, observacion: '' });
      alert('Estado actualizado correctamente.');
    } catch (error) {
      console.error('Error actualizando estado:', error);
      alert('No se pudo actualizar el estado.');
    }
  };

  if (loading) {
    return <Box sx={{ display: 'flex', justifyContent: 'center', p: 4 }}><CircularProgress /></Box>;
  }

  if (!servicio) {
    return <Typography>Servicio no encontrado.</Typography>;
  }

  return (
    <Box>
      <Box sx={{ display: 'flex', alignItems: 'center', mb: 3 }}>
        <Button startIcon={<ArrowBackIcon />} onClick={() => navigate('/servicios')} sx={{ mr: 2 }}>
          Volver
        </Button>
        <Typography variant="h4" sx={{ fontWeight: 'bold' }}>
          Tracking Interno: {servicio.serCodigo}
        </Typography>
      </Box>

      <Grid container spacing={3}>
        <Grid size={{ xs: 12, md: 8 }}>
          <Card sx={{ mb: 3 }}>
            <CardContent>
              <Typography variant="h6" sx={{ fontWeight: 'bold', mb: 2 }}>Información del Servicio</Typography>
              <Grid container spacing={2}>
                <Grid size={{ xs: 6 }}>
                  <Typography variant="body2" color="text.secondary">Tipo de Servicio</Typography>
                  <Typography variant="body1">{servicio.serTipoServicio}</Typography>
                </Grid>
                <Grid size={{ xs: 6 }}>
                  <Typography variant="body2" color="text.secondary">Prioridad</Typography>
                  <Typography variant="body1">{servicio.serPrioridad}</Typography>
                </Grid>
                <Grid size={{ xs: 6 }}>
                  <Typography variant="body2" color="text.secondary">Fecha Programada</Typography>
                  <Typography variant="body1">{new Date(servicio.fechaProgramada).toLocaleDateString()}</Typography>
                </Grid>
                <Grid size={{ xs: 6 }}>
                  <Typography variant="body2" color="text.secondary">Tracking Público (Copiar)</Typography>
                  <Typography variant="body1" sx={{ fontFamily: 'monospace' }}>
                    {servicio.trackingToken ? `https://tracking.jhtlogistics.com/${servicio.trackingToken}` : 'N/A'}
                  </Typography>
                </Grid>
              </Grid>
            </CardContent>
          </Card>

          <Card sx={{ mb: 3 }}>
            <CardContent>
              <Typography variant="h6" sx={{ fontWeight: 'bold', mb: 2 }}>Historial (Timeline)</Typography>
              <Stepper orientation="vertical">
                {servicio.historial?.map((hito: any, index: number) => (
                  <Step key={index} active={true}>
                    <StepLabel>
                      <Typography variant="subtitle2" sx={{ fontWeight: 'bold' }}>{hito.estadoNombre}</Typography>
                      <Typography variant="caption" color="text.secondary">{new Date(hito.fecha).toLocaleString()}</Typography>
                      {hito.observacion && <Typography variant="body2" sx={{ mt: 0.5 }}>{hito.observacion}</Typography>}
                    </StepLabel>
                  </Step>
                ))}
              </Stepper>
            </CardContent>
          </Card>
        </Grid>

        <Grid size={{ xs: 12, md: 4 }}>
          <Card sx={{ mb: 3 }}>
            <CardContent>
              <Typography variant="h6" sx={{ fontWeight: 'bold', mb: 2 }}>Actualizar Estado</Typography>
              <form onSubmit={handleSubmit(onUpdateState)}>
                <Controller
                  name="nuevoEstado"
                  control={control}
                  render={({ field }) => (
                    <TextField
                      {...field}
                      select
                      fullWidth
                      label="Nuevo Estado"
                      sx={{ mb: 2 }}
                    >
                      <MenuItem value={1}>Recepcionado</MenuItem>
                      <MenuItem value={2}>Programado</MenuItem>
                      <MenuItem value={3}>Unidad Asignada</MenuItem>
                      <MenuItem value={4}>En Ruta</MenuItem>
                      <MenuItem value={5}>Muy Cerca</MenuItem>
                      <MenuItem value={6}>Entregado</MenuItem>
                      <MenuItem value={7}>Cancelado</MenuItem>
                    </TextField>
                  )}
                />
                <Controller
                  name="observacion"
                  control={control}
                  render={({ field }) => (
                    <TextField
                      {...field}
                      fullWidth
                      multiline
                      rows={2}
                      label="Observación (Opcional)"
                      sx={{ mb: 2 }}
                    />
                  )}
                />
                <Button type="submit" variant="contained" color="primary" fullWidth>
                  Actualizar
                </Button>
              </form>
            </CardContent>
          </Card>
          
          <Card>
             <CardContent>
                <Typography variant="h6" sx={{ fontWeight: 'bold', mb: 2 }}>Asignaciones</Typography>
                <Typography variant="body2" color="text.secondary" sx={{ mb: 1 }}>Conductor Asignado:</Typography>
                <Typography variant="body1" sx={{ mb: 2 }}>{servicio.conductorNombre || 'Ninguno'}</Typography>
                
                <Typography variant="body2" color="text.secondary" sx={{ mb: 1 }}>Vehículo Asignado:</Typography>
                <Typography variant="body1" sx={{ mb: 3 }}>{servicio.vehiculoPlaca || 'Ninguno'}</Typography>

                <Button 
                  variant="outlined" 
                  fullWidth 
                  onClick={() => setOpenAssignDialog(true)}
                >
                  Asignar / Cambiar Unidad
                </Button>
             </CardContent>
          </Card>
        </Grid>
      </Grid>

      <AssignUnidadDialog
        open={openAssignDialog}
        onClose={() => setOpenAssignDialog(false)}
        serId={servicio.serId}
        onAssignSuccess={() => {
          setOpenAssignDialog(false);
          fetchServicio();
        }}
      />
    </Box>
  );
};

export default ServicioDetallePage;
