import { useEffect, useState } from 'react';
import { Box, Card, CardContent, Typography, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Chip, CircularProgress, Dialog, DialogTitle, DialogContent, Button, DialogActions } from '@mui/material';
import VisibilityIcon from '@mui/icons-material/Visibility';
import IconButton from '@mui/material/IconButton';
import api from '../../services/api';

const AuditoriaPage = () => {
  const [logs, setLogs] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [selectedLog, setSelectedLog] = useState<any>(null);

  useEffect(() => {
    // Para simplificar, en MVP pediremos todos los logs. En prod debe ser paginado.
    const fetchAuditoria = async () => {
      try {
        const response = await api.get('/auditoria');
        setLogs(response.data);
      } catch (error) {
        console.error('Error fetching auditoria:', error);
      } finally {
        setLoading(false);
      }
    };
    fetchAuditoria();
  }, []);

  const handleOpenJson = (log: any) => {
    setSelectedLog(log);
  };

  const renderJson = (jsonString: string) => {
    if (!jsonString) return 'Ninguno';
    try {
      const obj = JSON.parse(jsonString);
      return <pre style={{ margin: 0, fontSize: '0.8rem' }}>{JSON.stringify(obj, null, 2)}</pre>;
    } catch {
      return jsonString;
    }
  };

  return (
    <Box>
      <Typography variant="h4" sx={{ fontWeight: 'bold', mb: 3 }}>
        Auditoría Transaccional
      </Typography>

      <Card>
        <CardContent sx={{ p: 0 }}>
          {loading ? (
            <Box sx={{ display: 'flex', justifyContent: 'center', p: 4 }}><CircularProgress /></Box>
          ) : (
            <TableContainer component={Paper} elevation={0}>
              <Table>
                <TableHead sx={{ bgcolor: 'background.default' }}>
                  <TableRow>
                    <TableCell sx={{ fontWeight: 'bold' }}>ID / Fecha</TableCell>
                    <TableCell sx={{ fontWeight: 'bold' }}>Tabla</TableCell>
                    <TableCell sx={{ fontWeight: 'bold' }}>Acción</TableCell>
                    <TableCell sx={{ fontWeight: 'bold' }}>Usuario</TableCell>
                    <TableCell sx={{ fontWeight: 'bold', textAlign: 'center' }}>Cambios</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {logs.map((log) => (
                    <TableRow key={log.id} hover>
                      <TableCell>
                        <Typography variant="body2" sx={{ fontWeight: 'bold' }}>{log.id}</Typography>
                        <Typography variant="caption" color="text.secondary">{new Date(log.fecha).toLocaleString()}</Typography>
                      </TableCell>
                      <TableCell>
                        <Typography variant="body2">{log.nombreTabla}</Typography>
                        <Typography variant="caption" color="text.secondary">Reg ID: {log.registroId}</Typography>
                      </TableCell>
                      <TableCell>
                        <Chip
                          label={log.accion}
                          color={log.accion === 'INSERT' ? 'success' : log.accion === 'DELETE' ? 'error' : 'warning'}
                          size="small"
                        />
                      </TableCell>
                      <TableCell>{log.usuario}</TableCell>
                      <TableCell align="center">
                        <IconButton color="primary" onClick={() => handleOpenJson(log)}>
                          <VisibilityIcon />
                        </IconButton>
                      </TableCell>
                    </TableRow>
                  ))}
                  {logs.length === 0 && (
                    <TableRow>
                      <TableCell colSpan={5} align="center" sx={{ py: 3 }}>No hay registros de auditoría.</TableCell>
                    </TableRow>
                  )}
                </TableBody>
              </Table>
            </TableContainer>
          )}
        </CardContent>
      </Card>

      <Dialog open={!!selectedLog} onClose={() => setSelectedLog(null)} maxWidth="md" fullWidth>
        <DialogTitle>Detalle de Cambios (JSON)</DialogTitle>
        <DialogContent dividers>
           <Box sx={{ display: 'flex', gap: 2 }}>
             <Box sx={{ flex: 1 }}>
                <Typography variant="subtitle2" color="error">Valores Anteriores:</Typography>
                <Paper sx={{ p: 2, bgcolor: '#fafafa', overflowX: 'auto', mt: 1 }}>
                  {selectedLog && renderJson(selectedLog.valoresAnteriores)}
                </Paper>
             </Box>
             <Box sx={{ flex: 1 }}>
                <Typography variant="subtitle2" color="success.main">Valores Nuevos:</Typography>
                <Paper sx={{ p: 2, bgcolor: '#fafafa', overflowX: 'auto', mt: 1 }}>
                  {selectedLog && renderJson(selectedLog.valoresNuevos)}
                </Paper>
             </Box>
           </Box>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setSelectedLog(null)} color="primary">Cerrar</Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
};

export default AuditoriaPage;
