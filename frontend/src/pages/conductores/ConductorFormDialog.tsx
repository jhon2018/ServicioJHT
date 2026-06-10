import React, { useEffect } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
  Grid,
  MenuItem
} from '@mui/material';
import { useForm, Controller } from 'react-hook-form';
import * as yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import api from '../../services/api';
import type { Conductor } from './ConductoresPage';

interface ConductorFormDialogProps {
  open: boolean;
  onClose: () => void;
  onSaveSuccess: () => void;
  conductor: Conductor | null;
}

const schema = yup.object({
  conTipo: yup.string().required('Tipo es requerido'),
  conDni: yup.string().required('DNI es requerido'),
  conNombreCompleto: yup.string().required('Nombre completo es requerido'),
  conTelefono: yup.string().required('Teléfono es requerido'),
  conEmail: yup.string().email('Debe ser un email válido').nullable(),
  conCodigoExterno: yup.string().nullable()
});

const ConductorFormDialog: React.FC<ConductorFormDialogProps> = ({ open, onClose, onSaveSuccess, conductor }) => {
  const isEdit = !!conductor;

  const { control, handleSubmit, reset, formState: { errors, isSubmitting } } = useForm({
    resolver: yupResolver(schema),
    defaultValues: {
      conTipo: 'I',
      conDni: '',
      conNombreCompleto: '',
      conTelefono: '',
      conEmail: '',
      conCodigoExterno: ''
    }
  });

  useEffect(() => {
    if (conductor) {
      reset({
        conTipo: conductor.conTipo,
        conDni: conductor.conDni,
        conNombreCompleto: conductor.conNombreCompleto,
        conTelefono: conductor.conTelefono,
        conEmail: conductor.conEmail || '',
        conCodigoExterno: conductor.conCodigoExterno || ''
      });
    } else {
      reset({
        conTipo: 'I',
        conDni: '',
        conNombreCompleto: '',
        conTelefono: '',
        conEmail: '',
        conCodigoExterno: ''
      });
    }
  }, [conductor, reset]);

  const onSubmit = async (data: any) => {
    try {
      if (isEdit) {
        await api.put(`/conductores/${conductor.conId}`, { conId: conductor.conId, ...data });
      } else {
        await api.post('/conductores', data);
      }
      onSaveSuccess();
    } catch (error) {
      console.error('Error saving conductor:', error);
      alert('Error al guardar el conductor');
    }
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
      <DialogTitle>{isEdit ? 'Editar Conductor' : 'Nuevo Conductor'}</DialogTitle>
      <form onSubmit={handleSubmit(onSubmit)}>
        <DialogContent dividers>
          <Grid container spacing={2}>
            <Grid size={{ xs: 12, sm: 6 }}>
              <Controller
                name="conTipo"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    select
                    fullWidth
                    label="Tipo"
                    error={!!errors.conTipo}
                    helperText={errors.conTipo?.message as string}
                  >
                    <MenuItem value="I">Interno</MenuItem>
                    <MenuItem value="E">Externo</MenuItem>
                  </TextField>
                )}
              />
            </Grid>
            <Grid size={{ xs: 12, sm: 6 }}>
              <Controller
                name="conDni"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    fullWidth
                    label="DNI / Documento"
                    error={!!errors.conDni}
                    helperText={errors.conDni?.message as string}
                  />
                )}
              />
            </Grid>
            <Grid size={{ xs: 12 }}>
              <Controller
                name="conNombreCompleto"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    fullWidth
                    label="Nombre Completo"
                    error={!!errors.conNombreCompleto}
                    helperText={errors.conNombreCompleto?.message as string}
                  />
                )}
              />
            </Grid>
            <Grid size={{ xs: 12, sm: 6 }}>
              <Controller
                name="conTelefono"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    fullWidth
                    label="Teléfono"
                    error={!!errors.conTelefono}
                    helperText={errors.conTelefono?.message as string}
                  />
                )}
              />
            </Grid>
            <Grid size={{ xs: 12, sm: 6 }}>
              <Controller
                name="conEmail"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    fullWidth
                    label="Email"
                    error={!!errors.conEmail}
                    helperText={errors.conEmail?.message as string}
                  />
                )}
              />
            </Grid>
            <Grid size={{ xs: 12 }}>
              <Controller
                name="conCodigoExterno"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    fullWidth
                    label="Código Externo (Si aplica)"
                    error={!!errors.conCodigoExterno}
                    helperText={errors.conCodigoExterno?.message as string}
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

export default ConductorFormDialog;
