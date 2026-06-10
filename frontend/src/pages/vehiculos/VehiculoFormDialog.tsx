import React, { useEffect } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
  Grid
} from '@mui/material';
import { useForm, Controller } from 'react-hook-form';
import * as yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import api from '../../services/api';
import type { Vehiculo } from './VehiculosPage';

interface VehiculoFormDialogProps {
  open: boolean;
  onClose: () => void;
  onSaveSuccess: () => void;
  vehiculo: Vehiculo | null;
}

const schema = yup.object({
  vehPlaca: yup.string().required('La placa es requerida'),
  vehMarca: yup.string().required('La marca es requerida'),
  vehModelo: yup.string().required('El modelo es requerido'),
  vehTipo: yup.string().required('El tipo es requerido'),
  vehCapacidad: yup.string().nullable(),
});

const VehiculoFormDialog: React.FC<VehiculoFormDialogProps> = ({ open, onClose, onSaveSuccess, vehiculo }) => {
  const isEdit = !!vehiculo;

  const { control, handleSubmit, reset, formState: { errors, isSubmitting } } = useForm({
    resolver: yupResolver(schema),
    defaultValues: {
      vehPlaca: '',
      vehMarca: '',
      vehModelo: '',
      vehTipo: '',
      vehCapacidad: ''
    }
  });

  useEffect(() => {
    if (vehiculo) {
      reset({
        vehPlaca: vehiculo.vehPlaca,
        vehMarca: vehiculo.vehMarca,
        vehModelo: vehiculo.vehModelo,
        vehTipo: vehiculo.vehTipo,
        vehCapacidad: vehiculo.vehCapacidad || ''
      });
    } else {
      reset({
        vehPlaca: '',
        vehMarca: '',
        vehModelo: '',
        vehTipo: '',
        vehCapacidad: ''
      });
    }
  }, [vehiculo, reset]);

  const onSubmit = async (data: any) => {
    try {
      if (isEdit) {
        await api.put(`/vehiculos/${vehiculo.vehId}`, { vehId: vehiculo.vehId, ...data });
      } else {
        await api.post('/vehiculos', data);
      }
      onSaveSuccess();
    } catch (error) {
      console.error('Error saving vehiculo:', error);
      alert('Error al guardar el vehículo. Verifique si la placa ya existe.');
    }
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
      <DialogTitle>{isEdit ? 'Editar Vehículo' : 'Nuevo Vehículo'}</DialogTitle>
      <form onSubmit={handleSubmit(onSubmit)}>
        <DialogContent dividers>
          <Grid container spacing={2}>
            <Grid size={{ xs: 12, sm: 6 }}>
              <Controller
                name="vehPlaca"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    fullWidth
                    label="Placa"
                    error={!!errors.vehPlaca}
                    helperText={errors.vehPlaca?.message as string}
                    slotProps={{ htmlInput: { style: { textTransform: 'uppercase' } } }}
                  />
                )}
              />
            </Grid>
            <Grid size={{ xs: 12, sm: 6 }}>
              <Controller
                name="vehTipo"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    fullWidth
                    label="Tipo (Furgón, Camión, etc.)"
                    error={!!errors.vehTipo}
                    helperText={errors.vehTipo?.message as string}
                  />
                )}
              />
            </Grid>
            <Grid size={{ xs: 12, sm: 6 }}>
              <Controller
                name="vehMarca"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    fullWidth
                    label="Marca"
                    error={!!errors.vehMarca}
                    helperText={errors.vehMarca?.message as string}
                  />
                )}
              />
            </Grid>
            <Grid size={{ xs: 12, sm: 6 }}>
              <Controller
                name="vehModelo"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    fullWidth
                    label="Modelo"
                    error={!!errors.vehModelo}
                    helperText={errors.vehModelo?.message as string}
                  />
                )}
              />
            </Grid>
            <Grid size={{ xs: 12 }}>
              <Controller
                name="vehCapacidad"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    fullWidth
                    label="Capacidad (Toneladas, Metros Cúbicos)"
                    error={!!errors.vehCapacidad}
                    helperText={errors.vehCapacidad?.message as string}
                  />
                )}
              />
            </Grid>
          </Grid>
        </DialogContent>
        <DialogActions>
          <Button onClick={onClose} color="inherit">Cancelar</Button>
          <Button type="submit" variant="contained" color="primary" disabled={isSubmitting}>
            {isEdit ? 'Actualizar' : 'Guardar'}
          </Button>
        </DialogActions>
      </form>
    </Dialog>
  );
};

export default VehiculoFormDialog;
