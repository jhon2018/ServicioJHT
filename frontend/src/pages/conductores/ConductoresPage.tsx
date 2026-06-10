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
import ConductorFormDialog from './ConductorFormDialog';

export interface Conductor {
  conId: number;
  conCodigoExterno?: string;
  conTipo: string; // I = Interno, E = Externo
  conDni: string;
  conNombreCompleto: string;
  conTelefono: string;
  conEmail?: string;
  estado: boolean;
}

const ConductoresPage = () => {
  const [conductores, setConductores] = useState<Conductor[]>([]);
  const [loading, setLoading] = useState(true);
  const [openForm, setOpenForm] = useState(false);
  const [selectedConductor, setSelectedConductor] = useState<Conductor | null>(null);

  const fetchConductores = async () => {
    setLoading(true);
    try {
      const response = await api.get('/conductores');
      setConductores(response.data);
    } catch (error) {
      console.error('Error fetching conductores:', error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchConductores();
  }, []);

  const handleOpenForm = (conductor?: Conductor) => {
    if (conductor) {
      setSelectedConductor(conductor);
    } else {
      setSelectedConductor(null);
    }
    setOpenForm(true);
  };

  const handleCloseForm = () => {
    setOpenForm(false);
    setSelectedConductor(null);
  };

  const handleSaveSuccess = () => {
    handleCloseForm();
    fetchConductores();
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('¿Está seguro de desactivar este conductor?')) {
      try {
        await api.delete(`/conductores/${id}`);
        fetchConductores();
      } catch (error) {
        console.error('Error deleting conductor:', error);
      }
    }
  };

  return (
    <Box>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
        <Typography variant="h4" sx={{ fontWeight: 'bold' }}>
          Conductores
        </Typography>
        <Button
          variant="contained"
          color="primary"
          startIcon={<AddIcon />}
          onClick={() => handleOpenForm()}
        >
          Nuevo Conductor
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
                    <TableCell sx={{ fontWeight: 'bold' }}>Documento</TableCell>
                    <TableCell sx={{ fontWeight: 'bold' }}>Nombre</TableCell>
                    <TableCell sx={{ fontWeight: 'bold' }}>Tipo</TableCell>
                    <TableCell sx={{ fontWeight: 'bold' }}>Contacto</TableCell>
                    <TableCell sx={{ fontWeight: 'bold' }}>Estado</TableCell>
                    <TableCell sx={{ fontWeight: 'bold', textAlign: 'right' }}>Acciones</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {conductores.map((conductor) => (
                    <TableRow key={conductor.conId} hover>
                      <TableCell>{conductor.conDni}</TableCell>
                      <TableCell>{conductor.conNombreCompleto}</TableCell>
                      <TableCell>
                        <Chip
                          label={conductor.conTipo === 'I' ? 'Interno' : 'Externo'}
                          color={conductor.conTipo === 'I' ? 'info' : 'default'}
                          size="small"
                        />
                      </TableCell>
                      <TableCell>
                        <Box>{conductor.conTelefono}</Box>
                        {conductor.conEmail && <Box sx={{ fontSize: '0.8rem', color: 'text.secondary' }}>{conductor.conEmail}</Box>}
                      </TableCell>
                      <TableCell>
                        <Chip
                          label={conductor.estado ? 'Activo' : 'Inactivo'}
                          color={conductor.estado ? 'success' : 'error'}
                          size="small"
                        />
                      </TableCell>
                      <TableCell align="right">
                        <IconButton color="primary" onClick={() => handleOpenForm(conductor)} size="small">
                          <EditIcon fontSize="small" />
                        </IconButton>
                        {conductor.estado && (
                          <IconButton color="error" onClick={() => handleDelete(conductor.conId)} size="small">
                            <DeleteIcon fontSize="small" />
                          </IconButton>
                        )}
                      </TableCell>
                    </TableRow>
                  ))}
                  {conductores.length === 0 && (
                    <TableRow>
                      <TableCell colSpan={6} align="center" sx={{ py: 3 }}>
                        No hay conductores registrados.
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
        <ConductorFormDialog
          open={openForm}
          onClose={handleCloseForm}
          onSaveSuccess={handleSaveSuccess}
          conductor={selectedConductor}
        />
      )}
    </Box>
  );
};

export default ConductoresPage;
