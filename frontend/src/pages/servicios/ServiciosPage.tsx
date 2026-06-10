import { useEffect, useState } from 'react';
import {
  Box,
  Button,
  Card,
  CardContent,
  Typography,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  IconButton,
  Chip,
  CircularProgress
} from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import VisibilityIcon from '@mui/icons-material/Visibility';
import api from '../../services/api';
import ServicioFormDialog from './ServicioFormDialog';
import { useNavigate } from 'react-router-dom';

export interface Servicio {
  serId: number;
  serCodigo: string;
  cliId: number;
  serTipoServicio: string;
  serPrioridad: string;
  estadoNombre: string;
  fechaProgramada: string;
  destinos?: any[];
}

const ServiciosPage = () => {
  const [servicios, setServicios] = useState<Servicio[]>([]);
  const [loading, setLoading] = useState(true);
  const [openForm, setOpenForm] = useState(false);
  const navigate = useNavigate();

  const fetchServicios = async () => {
    setLoading(true);
    try {
      const response = await api.get('/servicios');
      setServicios(response.data);
    } catch (error) {
      console.error('Error fetching servicios:', error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchServicios();
  }, []);

  const handleOpenForm = () => {
    setOpenForm(true);
  };

  const handleCloseForm = () => {
    setOpenForm(false);
  };

  const handleSaveSuccess = () => {
    handleCloseForm();
    fetchServicios();
  };

  const getPriorityColor = (prioridad: string) => {
    switch (prioridad) {
      case 'ALTA': return 'error';
      case 'MEDIA': return 'warning';
      case 'BAJA': return 'info';
      default: return 'default';
    }
  };

  return (
    <Box>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
        <Typography variant="h4" sx={{ fontWeight: 'bold' }}>
          Servicios (Órdenes)
        </Typography>
        <Button
          variant="contained"
          color="primary"
          startIcon={<AddIcon />}
          onClick={handleOpenForm}
        >
          Crear Servicio
        </Button>
      </Box>

      <Card>
        <CardContent sx={{ p: 0 }}>
          {loading ? (
            <Box sx={{ display: 'flex', justifyContent: 'center', p: 4 }}>
              <CircularProgress />
            </Box>
          ) : (
            <TableContainer component={Paper} elevation={0}>
              <Table>
                <TableHead sx={{ bgcolor: 'background.default' }}>
                  <TableRow>
                    <TableCell sx={{ fontWeight: 'bold' }}>Código</TableCell>
                    <TableCell sx={{ fontWeight: 'bold' }}>Tipo / Prioridad</TableCell>
                    <TableCell sx={{ fontWeight: 'bold' }}>Fecha Programada</TableCell>
                    <TableCell sx={{ fontWeight: 'bold' }}>Estado</TableCell>
                    <TableCell sx={{ fontWeight: 'bold', textAlign: 'right' }}>Acciones</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {servicios.map((servicio) => (
                    <TableRow key={servicio.serId} hover>
                      <TableCell>
                        <Typography variant="body2" sx={{ fontWeight: 'bold' }}>
                          {servicio.serCodigo}
                        </Typography>
                      </TableCell>
                      <TableCell>
                        {servicio.serTipoServicio}
                        <br />
                        <Chip
                          label={servicio.serPrioridad}
                          color={getPriorityColor(servicio.serPrioridad)}
                          size="small"
                          sx={{ mt: 0.5 }}
                        />
                      </TableCell>
                      <TableCell>
                        {new Date(servicio.fechaProgramada).toLocaleDateString()}
                      </TableCell>
                      <TableCell>
                        <Chip label={servicio.estadoNombre} color="primary" variant="outlined" size="small" />
                      </TableCell>
                      <TableCell align="right">
                        <IconButton 
                          color="info" 
                          onClick={() => navigate(`/servicios/${servicio.serId}`)} 
                          size="small" 
                          title="Tracking Interno"
                        >
                          <VisibilityIcon fontSize="small" />
                        </IconButton>
                      </TableCell>
                    </TableRow>
                  ))}
                  {servicios.length === 0 && (
                    <TableRow>
                      <TableCell colSpan={5} align="center" sx={{ py: 3 }}>
                        No hay servicios registrados.
                      </TableCell>
                    </TableRow>
                  )}
                </TableBody>
              </Table>
            </TableContainer>
          )}
        </CardContent>
      </Card>

      {openForm && (
        <ServicioFormDialog
          open={openForm}
          onClose={handleCloseForm}
          onSaveSuccess={handleSaveSuccess}
        />
      )}
    </Box>
  );
};

export default ServiciosPage;
