import React, { useEffect, useState } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
  MenuItem,
  CircularProgress,
  Box
} from '@mui/material';
import api from '../../services/api';

interface AssignUnidadDialogProps {
  open: boolean;
  onClose: () => void;
  onAssignSuccess: () => void;
  serId: number;
}

const AssignUnidadDialog: React.FC<AssignUnidadDialogProps> = ({ open, onClose, onAssignSuccess, serId }) => {
  const [conductores, setConductores] = useState<any[]>([]);
  const [vehiculos, setVehiculos] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  
  const [selectedConductor, setSelectedConductor] = useState('');
  const [selectedVehiculo, setSelectedVehiculo] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      try {
        const [condRes, vehRes] = await Promise.all([
          api.get('/conductores'),
          api.get('/vehiculos')
        ]);
        // Solo mostrar los activos si la data tiene estado
        setConductores(condRes.data.filter((c: any) => c.estado !== false));
        setVehiculos(vehRes.data.filter((v: any) => v.estado !== false));
      } catch (error) {
        console.error('Error fetching data for assignment:', error);
      } finally {
        setLoading(false);
      }
    };

    if (open) {
      fetchData();
      setSelectedConductor('');
      setSelectedVehiculo('');
    }
  }, [open]);

  const handleAssign = async () => {
    if (!selectedConductor || !selectedVehiculo) {
      alert('Debe seleccionar conductor y vehículo');
      return;
    }
    
    setIsSubmitting(true);
    try {
      await api.post(`/servicios/${serId}/assign`, {
        conId: selectedConductor,
        vehId: selectedVehiculo
      });
      onAssignSuccess();
    } catch (error) {
      console.error('Error asigando unidad:', error);
      alert('Error al realizar la asignación');
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
      <DialogTitle>Asignar Conductor y Vehículo</DialogTitle>
      <DialogContent dividers>
        {loading ? (
          <Box sx={{ display: 'flex', justifyContent: 'center', p: 3 }}>
            <CircularProgress />
          </Box>
        ) : (
          <Box sx={{ pt: 1 }}>
            <TextField
              select
              fullWidth
              label="Seleccionar Conductor"
              value={selectedConductor}
              onChange={(e) => setSelectedConductor(e.target.value)}
              sx={{ mb: 3 }}
            >
              {conductores.map((c) => (
                <MenuItem key={c.conId} value={c.conId}>
                  {c.conNumeroDocumento} - {c.conNombres} {c.conApellidos} ({c.conTipo})
                </MenuItem>
              ))}
            </TextField>
            
            <TextField
              select
              fullWidth
              label="Seleccionar Vehículo"
              value={selectedVehiculo}
              onChange={(e) => setSelectedVehiculo(e.target.value)}
            >
              {vehiculos.map((v) => (
                <MenuItem key={v.vehId} value={v.vehId}>
                  {v.vehPlaca} - {v.vehMarca} {v.vehModelo} ({v.vehTipo})
                </MenuItem>
              ))}
            </TextField>
          </Box>
        )}
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose} color="inherit">Cancelar</Button>
        <Button 
          onClick={handleAssign} 
          variant="contained" 
          color="primary" 
          disabled={loading || isSubmitting || !selectedConductor || !selectedVehiculo}
        >
          Asignar
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default AssignUnidadDialog;
