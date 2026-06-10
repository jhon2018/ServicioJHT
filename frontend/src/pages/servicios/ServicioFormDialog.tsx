import React, { useEffect, useState } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
  Grid,
  MenuItem,
  Typography,
  Box,
  IconButton,
  Divider,
  Card
} from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import AddIcon from '@mui/icons-material/Add';
import { useForm, Controller, useFieldArray } from 'react-hook-form';
import * as yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import api from '../../services/api';

interface ServicioFormDialogProps {
  open: boolean;
  onClose: () => void;
  onSaveSuccess: () => void;
}

const destinoSchema = yup.object({
  destinatario: yup.string().required('Requerido'),
  direccion: yup.string().required('Requerida'),
  coordenadas: yup.string().nullable()
});

const schema = yup.object({
  cliId: yup.number().required('El cliente es requerido').min(1, 'Seleccione un cliente'),
  serTipoServicio: yup.string().required('El tipo es requerido'),
  serPrioridad: yup.string().required('La prioridad es requerida'),
  serObservaciones: yup.string().nullable(),
  fechaProgramada: yup.string().required('Fecha programada requerida'),
  destinos: yup.array().of(destinoSchema).min(1, 'Debe agregar al menos un destino')
});

const ServicioFormDialog: React.FC<ServicioFormDialogProps> = ({ open, onClose, onSaveSuccess }) => {
  const [clientes, setClientes] = useState<any[]>([]);

  const { control, handleSubmit, formState: { errors, isSubmitting } } = useForm({
    resolver: yupResolver(schema),
    defaultValues: {
      cliId: 0,
      serTipoServicio: 'TRASLADO',
      serPrioridad: 'MEDIA',
      serObservaciones: '',
      fechaProgramada: new Date().toISOString().split('T')[0],
      destinos: [{ destinatario: '', direccion: '', coordenadas: '' }]
    }
  });

  const { fields, append, remove } = useFieldArray({
    control,
    name: 'destinos'
  });

  useEffect(() => {
    const fetchClientes = async () => {
      try {
        const response = await api.get('/clientes');
        setClientes(response.data.filter((c: any) => c.estado));
      } catch (error) {
        console.error('Error fetching clientes:', error);
      }
    };
    if (open) fetchClientes();
  }, [open]);

  const onSubmit = async (data: any) => {
    try {
      // Parsear la fecha a ISO
      const payload = {
        ...data,
        fechaProgramada: new Date(data.fechaProgramada).toISOString()
      };
      await api.post('/servicios', payload);
      onSaveSuccess();
    } catch (error) {
      console.error('Error saving servicio:', error);
      alert('Error al guardar la orden de servicio.');
    }
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="md" fullWidth>
      <DialogTitle>Nueva Orden de Servicio</DialogTitle>
      <form onSubmit={handleSubmit(onSubmit)}>
        <DialogContent dividers>
          <Typography variant="subtitle1" sx={{ fontWeight: 'bold', mb: 2 }}>Datos Generales</Typography>
          <Grid container spacing={2}>
            <Grid size={{ xs: 12, sm: 6 }}>
              <Controller
                name="cliId"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    select
                    fullWidth
                    label="Cliente"
                    error={!!errors.cliId}
                    helperText={errors.cliId?.message as string}
                  >
                    <MenuItem value={0} disabled>Seleccione un cliente</MenuItem>
                    {clientes.map((cli) => (
                      <MenuItem key={cli.cliId} value={cli.cliId}>
                        {cli.cliRazonSocial} ({cli.cliNumeroDocumento})
                      </MenuItem>
                    ))}
                  </TextField>
                )}
              />
            </Grid>
            <Grid size={{ xs: 12, sm: 6 }}>
              <Controller
                name="fechaProgramada"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    type="date"
                    fullWidth
                    label="Fecha Programada"
                    slotProps={{ inputLabel: { shrink: true } }}
                    error={!!errors.fechaProgramada}
                    helperText={errors.fechaProgramada?.message as string}
                  />
                )}
              />
            </Grid>
            <Grid size={{ xs: 12, sm: 6 }}>
              <Controller
                name="serTipoServicio"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    select
                    fullWidth
                    label="Tipo de Servicio"
                    error={!!errors.serTipoServicio}
                    helperText={errors.serTipoServicio?.message as string}
                  >
                    <MenuItem value="TRASLADO">Traslado</MenuItem>
                    <MenuItem value="DISTRIBUCION">Distribución</MenuItem>
                    <MenuItem value="EXPRESS">Express</MenuItem>
                  </TextField>
                )}
              />
            </Grid>
            <Grid size={{ xs: 12, sm: 6 }}>
              <Controller
                name="serPrioridad"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    select
                    fullWidth
                    label="Prioridad"
                    error={!!errors.serPrioridad}
                    helperText={errors.serPrioridad?.message as string}
                  >
                    <MenuItem value="BAJA">Baja</MenuItem>
                    <MenuItem value="MEDIA">Media</MenuItem>
                    <MenuItem value="ALTA">Alta</MenuItem>
                  </TextField>
                )}
              />
            </Grid>
            <Grid size={{ xs: 12 }}>
              <Controller
                name="serObservaciones"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    fullWidth
                    multiline
                    rows={2}
                    label="Observaciones"
                    error={!!errors.serObservaciones}
                    helperText={errors.serObservaciones?.message as string}
                  />
                )}
              />
            </Grid>
          </Grid>

          <Divider sx={{ my: 3 }} />

          <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 2 }}>
            <Typography variant="subtitle1" sx={{ fontWeight: 'bold' }}>Destinos de Entrega</Typography>
            <Button size="small" startIcon={<AddIcon />} onClick={() => append({ destinatario: '', direccion: '', coordenadas: '' })}>
              Agregar Destino
            </Button>
          </Box>
          
          {typeof errors.destinos?.message === 'string' && (
             <Typography color="error" variant="caption" sx={{ display: 'block', mb: 1 }}>{errors.destinos.message}</Typography>
          )}

          {fields.map((item, index) => (
            <Card key={item.id} variant="outlined" sx={{ mb: 2, p: 2, bgcolor: 'background.default' }}>
              <Grid container spacing={2} sx={{ alignItems: 'center' }}>
                <Grid size={{ xs: 12, sm: 4 }}>
                  <Controller
                    name={`destinos.${index}.destinatario`}
                    control={control}
                    render={({ field }) => (
                      <TextField
                        {...field}
                        fullWidth
                        size="small"
                        label="Destinatario"
                        error={!!errors?.destinos?.[index]?.destinatario}
                      />
                    )}
                  />
                </Grid>
                <Grid size={{ xs: 12, sm: 5 }}>
                  <Controller
                    name={`destinos.${index}.direccion`}
                    control={control}
                    render={({ field }) => (
                      <TextField
                        {...field}
                        fullWidth
                        size="small"
                        label="Dirección"
                        error={!!errors?.destinos?.[index]?.direccion}
                      />
                    )}
                  />
                </Grid>
                <Grid size={{ xs: 10, sm: 2 }}>
                  <Controller
                    name={`destinos.${index}.coordenadas`}
                    control={control}
                    render={({ field }) => (
                      <TextField
                        {...field}
                        fullWidth
                        size="small"
                        label="Coordenadas (Opcional)"
                      />
                    )}
                  />
                </Grid>
                <Grid sx={{ textAlign: 'center' }} size={{ xs: 2, sm: 1 }}>
                  <IconButton color="error" onClick={() => remove(index)} disabled={fields.length === 1}>
                    <DeleteIcon />
                  </IconButton>
                </Grid>
              </Grid>
            </Card>
          ))}
        </DialogContent>
        <DialogActions>
          <Button onClick={onClose} color="inherit">Cancelar</Button>
          <Button type="submit" variant="contained" color="primary" disabled={isSubmitting}>
            Generar Orden
          </Button>
        </DialogActions>
      </form>
    </Dialog>
  );
};

export default ServicioFormDialog;
