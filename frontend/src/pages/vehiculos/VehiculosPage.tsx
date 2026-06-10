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
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import api from '../../services/api';
import VehiculoFormDialog from './VehiculoFormDialog';

export interface Vehiculo {
  vehId: number;
  vehPlaca: string;
  vehMarca: string;
  vehModelo: string;
  vehTipo: string;
  vehCapacidad?: string;
  estado: boolean;
}

const VehiculosPage = () => {
  const [vehiculos, setVehiculos] = useState<Vehiculo[]>([]);
  const [loading, setLoading] = useState(true);
  const [openForm, setOpenForm] = useState(false);
  const [selectedVehiculo, setSelectedVehiculo] = useState<Vehiculo | null>(null);

  const fetchVehiculos = async () => {
    setLoading(true);
    try {
      const response = await api.get('/vehiculos');
      setVehiculos(response.data);
    } catch (error) {
      console.error('Error fetching vehiculos:', error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchVehiculos();
  }, []);

  const handleOpenForm = (vehiculo?: Vehiculo) => {
    if (vehiculo) {
      setSelectedVehiculo(vehiculo);
    } else {
      setSelectedVehiculo(null);
    }
    setOpenForm(true);
  };

  const handleCloseForm = () => {
    setOpenForm(false);
    setSelectedVehiculo(null);
  };

  const handleSaveSuccess = () => {
    handleCloseForm();
    fetchVehiculos();
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('¿Está seguro de desactivar este vehículo?')) {
      try {
        await api.delete(`/vehiculos/${id}`);
        fetchVehiculos();
      } catch (error) {
        console.error('Error deleting vehiculo:', error);
      }
    }
  };

  return (
    <Box>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
        <Typography variant="h4" sx={{ fontWeight: 'bold' }}>
          Vehículos
        </Typography>
        <Button
          variant="contained"
          color="primary"
          startIcon={<AddIcon />}
          onClick={() => handleOpenForm()}
        >
          Nuevo Vehículo
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
                    <TableCell sx={{ fontWeight: 'bold' }}>Placa</TableCell>
                    <TableCell sx={{ fontWeight: 'bold' }}>Marca / Modelo</TableCell>
                    <TableCell sx={{ fontWeight: 'bold' }}>Tipo</TableCell>
                    <TableCell sx={{ fontWeight: 'bold' }}>Capacidad</TableCell>
                    <TableCell sx={{ fontWeight: 'bold' }}>Estado</TableCell>
                    <TableCell sx={{ fontWeight: 'bold', textAlign: 'right' }}>Acciones</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {vehiculos.map((vehiculo) => (
                    <TableRow key={vehiculo.vehId} hover>
                      <TableCell>
                        <Chip label={vehiculo.vehPlaca} variant="outlined" color="primary" />
                      </TableCell>
                      <TableCell>
                        <Box sx={{ fontWeight: 'bold' }}>{vehiculo.vehMarca}</Box>
                        <Box sx={{ fontSize: '0.8rem', color: 'text.secondary' }}>{vehiculo.vehModelo}</Box>
                      </TableCell>
                      <TableCell>{vehiculo.vehTipo}</TableCell>
                      <TableCell>{vehiculo.vehCapacidad || 'N/A'}</TableCell>
                      <TableCell>
                        <Chip
                          label={vehiculo.estado ? 'Activo' : 'Inactivo'}
                          color={vehiculo.estado ? 'success' : 'error'}
                          size="small"
                        />
                      </TableCell>
                      <TableCell align="right">
                        <IconButton color="primary" onClick={() => handleOpenForm(vehiculo)} size="small">
                          <EditIcon fontSize="small" />
                        </IconButton>
                        {vehiculo.estado && (
                          <IconButton color="error" onClick={() => handleDelete(vehiculo.vehId)} size="small">
                            <DeleteIcon fontSize="small" />
                          </IconButton>
                        )}
                      </TableCell>
                    </TableRow>
                  ))}
                  {vehiculos.length === 0 && (
                    <TableRow>
                      <TableCell colSpan={6} align="center" sx={{ py: 3 }}>
                        No hay vehículos registrados.
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
        <VehiculoFormDialog
          open={openForm}
          onClose={handleCloseForm}
          onSaveSuccess={handleSaveSuccess}
          vehiculo={selectedVehiculo}
        />
      )}
    </Box>
  );
};

export default VehiculosPage;
