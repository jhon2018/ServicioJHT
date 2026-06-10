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
import ClienteFormDialog from './ClienteFormDialog';

export interface Cliente {
  cliId: number;
  cliTipoDocumento: string;
  cliNumeroDocumento: string;
  cliRazonSocial: string;
  cliNombreComercial?: string;
  cliDireccion?: string;
  cliTelefono?: string;
  cliEmail?: string;
  estado: boolean;
}

const ClientesPage = () => {
  const [clientes, setClientes] = useState<Cliente[]>([]);
  const [loading, setLoading] = useState(true);
  const [openForm, setOpenForm] = useState(false);
  const [selectedCliente, setSelectedCliente] = useState<Cliente | null>(null);

  const fetchClientes = async () => {
    setLoading(true);
    try {
      const response = await api.get('/clientes');
      setClientes(response.data);
    } catch (error) {
      console.error('Error fetching clientes:', error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchClientes();
  }, []);

  const handleOpenForm = (cliente?: Cliente) => {
    if (cliente) {
      setSelectedCliente(cliente);
    } else {
      setSelectedCliente(null);
    }
    setOpenForm(true);
  };

  const handleCloseForm = () => {
    setOpenForm(false);
    setSelectedCliente(null);
  };

  const handleSaveSuccess = () => {
    handleCloseForm();
    fetchClientes();
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('¿Está seguro de desactivar este cliente?')) {
      try {
        await api.delete(`/clientes/${id}`);
        fetchClientes();
      } catch (error) {
        console.error('Error deleting cliente:', error);
      }
    }
  };

  return (
    <Box>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
        <Typography variant="h4" sx={{ fontWeight: 'bold' }}>
          Clientes
        </Typography>
        <Button
          variant="contained"
          color="primary"
          startIcon={<AddIcon />}
          onClick={() => handleOpenForm()}
        >
          Nuevo Cliente
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
                    <TableCell sx={{ fontWeight: 'bold' }}>Razón Social / Nombre</TableCell>
                    <TableCell sx={{ fontWeight: 'bold' }}>Contacto</TableCell>
                    <TableCell sx={{ fontWeight: 'bold' }}>Estado</TableCell>
                    <TableCell sx={{ fontWeight: 'bold', textAlign: 'right' }}>Acciones</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {clientes.map((cliente) => (
                    <TableRow key={cliente.cliId} hover>
                      <TableCell>
                        <Typography variant="body2" sx={{ fontWeight: 'bold' }}>
                          {cliente.cliTipoDocumento}
                        </Typography>
                        {cliente.cliNumeroDocumento}
                      </TableCell>
                      <TableCell>
                        {cliente.cliRazonSocial}
                        {cliente.cliNombreComercial && (
                          <Typography variant="caption" sx={{ display: 'block' }} color="text.secondary">
                            {cliente.cliNombreComercial}
                          </Typography>
                        )}
                      </TableCell>
                      <TableCell>
                        {cliente.cliTelefono && <Box>{cliente.cliTelefono}</Box>}
                        {cliente.cliEmail && <Box sx={{ fontSize: '0.8rem', color: 'text.secondary' }}>{cliente.cliEmail}</Box>}
                      </TableCell>
                      <TableCell>
                        <Chip
                          label={cliente.estado ? 'Activo' : 'Inactivo'}
                          color={cliente.estado ? 'success' : 'error'}
                          size="small"
                        />
                      </TableCell>
                      <TableCell align="right">
                        <IconButton color="primary" onClick={() => handleOpenForm(cliente)} size="small">
                          <EditIcon fontSize="small" />
                        </IconButton>
                        {cliente.estado && (
                          <IconButton color="error" onClick={() => handleDelete(cliente.cliId)} size="small">
                            <DeleteIcon fontSize="small" />
                          </IconButton>
                        )}
                      </TableCell>
                    </TableRow>
                  ))}
                  {clientes.length === 0 && (
                    <TableRow>
                      <TableCell colSpan={5} align="center" sx={{ py: 3 }}>
                        No hay clientes registrados.
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
        <ClienteFormDialog
          open={openForm}
          onClose={handleCloseForm}
          onSaveSuccess={handleSaveSuccess}
          cliente={selectedCliente}
        />
      )}
    </Box>
  );
};

export default ClientesPage;
