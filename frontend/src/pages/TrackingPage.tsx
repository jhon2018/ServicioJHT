import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { Card, CardContent, Typography, CircularProgress, Box, Stepper, Step, StepLabel } from '@mui/material';
import api from '../services/api';

const TrackingPage = () => {
  const { token } = useParams<{ token: string }>();
  const [data, setData] = useState<any>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchTracking = async () => {
      try {
        const response = await api.get(`/tracking/${token}`);
        setData(response.data);
      } catch (err: any) {
        if (err.response && err.response.status === 404) {
          setError('Link de tracking inválido, expirado o no encontrado.');
        } else {
          setError('Error de conexión.');
        }
      } finally {
        setLoading(false);
      }
    };
    fetchTracking();
  }, [token]);

  if (loading) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', p: 5 }}>
        <CircularProgress />
      </Box>
    );
  }

  if (error) {
    return (
      <Card sx={{ p: 3, textAlign: 'center' }}>
        <Typography variant="h5" color="error" sx={{ mb: 2 }}>Información no disponible</Typography>
        <Typography>{error}</Typography>
      </Card>
    );
  }

  if (!data) return null;

  return (
    <Box>
      <Card sx={{ mb: 3 }}>
        <CardContent>
          <Typography variant="h5" sx={{ fontWeight: 'bold', mb: 1 }}>
            Servicio: {data.serCodigo}
          </Typography>
          <Typography variant="subtitle1" color="text.secondary">
            Estado Actual: <Box component="span" sx={{ fontWeight: 'bold', color: 'primary.main' }}>{data.estadoNombre}</Box>
          </Typography>
          <Typography variant="body2" color="text.secondary">
            Programado para: {new Date(data.fechaProgramada).toLocaleDateString()}
          </Typography>
        </CardContent>
      </Card>

      <Typography variant="h6" sx={{ mb: 2, fontWeight: 'bold' }}>Destinos</Typography>
      {data.destinos?.map((destino: any, index: number) => (
        <Card key={index} sx={{ mb: 2 }}>
          <CardContent>
            <Typography variant="subtitle1" sx={{ fontWeight: 'bold' }}>{destino.destinatario}</Typography>
            <Typography variant="body2">{destino.direccion}</Typography>
            <Typography variant="caption" sx={{ display: 'block', mt: 1, color: destino.entregado ? 'success.main' : 'warning.main' }}>
              {destino.entregado ? 'Entregado' : 'Pendiente'}
            </Typography>
          </CardContent>
        </Card>
      ))}

      <Typography variant="h6" sx={{ mt: 4, mb: 2, fontWeight: 'bold' }}>Historial Operativo</Typography>
      <Card>
        <CardContent>
          <Stepper orientation="vertical">
            {data.historial?.map((hito: any, index: number) => (
              <Step key={index} active={true}>
                <StepLabel>
                  <Typography variant="subtitle2">{hito.estadoNombre}</Typography>
                  <Typography variant="caption" color="text.secondary">{new Date(hito.fecha).toLocaleString()}</Typography>
                  {hito.observacion && <Typography variant="body2">{hito.observacion}</Typography>}
                </StepLabel>
              </Step>
            ))}
          </Stepper>
        </CardContent>
      </Card>
    </Box>
  );
};

export default TrackingPage;
