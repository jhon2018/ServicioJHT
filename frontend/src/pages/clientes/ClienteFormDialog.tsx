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
import type { Cliente } from './ClientesPage';

interface ClienteFormDialogProps {
  open: boolean;
  onClose: () => void;
  onSaveSuccess: () => void;
  cliente: Cliente | null;
}

const schema = yup.object({
  cliTipoDocumento: yup.string().required('Tipo es requerido'),
  cliNumeroDocumento: yup.string().required('Número es requerido'),
  cliRazonSocial: yup.string().required('Razón Social / Nombre es requerido'),
  cliNombreComercial: yup.string().nullable(),
  cliDireccion: yup.string().nullable(),
  cliTelefono: yup.string().nullable(),
  cliEmail: yup.string().email('Debe ser un email válido').nullable(),
});

const ClienteFormDialog: React.FC<ClienteFormDialogProps> = ({ open, onClose, onSaveSuccess, cliente }) => {
  const isEdit = !!cliente;

  const { control, handleSubmit, reset, formState: { errors, isSubmitting } } = useForm({
    resolver: yupResolver(schema),
    defaultValues: {
      cliTipoDocumento: 'DNI',
      cliNumeroDocumento: '',
      cliRazonSocial: '',
      cliNombreComercial: '',
      cliDireccion: '',
      cliTelefono: '',
      cliEmail: '',
    }
  });

  useEffect(() => {
    if (cliente) {
      reset({
        cliTipoDocumento: cliente.cliTipoDocumento,
        cliNumeroDocumento: cliente.cliNumeroDocumento,
        cliRazonSocial: cliente.cliRazonSocial,
        cliNombreComercial: cliente.cliNombreComercial || '',
        cliDireccion: cliente.cliDireccion || '',
        cliTelefono: cliente.cliTelefono || '',
        cliEmail: cliente.cliEmail || '',
      });
    } else {
      reset({
        cliTipoDocumento: 'DNI',
        cliNumeroDocumento: '',
        cliRazonSocial: '',
        cliNombreComercial: '',
        cliDireccion: '',
        cliTelefono: '',
        cliEmail: '',
      });
    }
  }, [cliente, reset]);

  const onSubmit = async (data: any) => {
    try {
      if (isEdit) {
        await api.put(`/clientes/${cliente.cliId}`, { cliId: cliente.cliId, ...data });
      } else {
        await api.post('/clientes', data);
      }
      onSaveSuccess();
    } catch (error) {
      console.error('Error saving cliente:', error);
      alert('Error al guardar el cliente');
    }
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
      <DialogTitle>{isEdit ? 'Editar Cliente' : 'Nuevo Cliente'}</DialogTitle>
      <form onSubmit={handleSubmit(onSubmit)}>
        <DialogContent dividers>
          <Grid container spacing={2}>
            <Grid size={{ xs: 12, sm: 4 }}>
              <Controller
                name="cliTipoDocumento"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    select
                    fullWidth
                    label="Tipo Doc."
                    error={!!errors.cliTipoDocumento}
                    helperText={errors.cliTipoDocumento?.message as string}
                  >
                    <MenuItem value="DNI">DNI (Natural)</MenuItem>
                    <MenuItem value="RUC">RUC (Empresa)</MenuItem>
                    <MenuItem value="CE">CE</MenuItem>
                  </TextField>
                )}
              />
            </Grid>
            <Grid size={{ xs: 12, sm: 8 }}>
              <Controller
                name="cliNumeroDocumento"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    fullWidth
                    label="Número Documento"
                    error={!!errors.cliNumeroDocumento}
                    helperText={errors.cliNumeroDocumento?.message as string}
                  />
                )}
              />
            </Grid>
            <Grid size={{ xs: 12 }}>
              <Controller
                name="cliRazonSocial"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    fullWidth
                    label="Razón Social / Nombre Completo"
                    error={!!errors.cliRazonSocial}
                    helperText={errors.cliRazonSocial?.message as string}
                  />
                )}
              />
            </Grid>
            <Grid size={{ xs: 12 }}>
              <Controller
                name="cliNombreComercial"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    fullWidth
                    label="Nombre Comercial (Opcional)"
                    error={!!errors.cliNombreComercial}
                    helperText={errors.cliNombreComercial?.message as string}
                  />
                )}
              />
            </Grid>
            <Grid size={{ xs: 12 }}>
              <Controller
                name="cliDireccion"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    fullWidth
                    label="Dirección"
                    error={!!errors.cliDireccion}
                    helperText={errors.cliDireccion?.message as string}
                  />
                )}
              />
            </Grid>
            <Grid size={{ xs: 12, sm: 6 }}>
              <Controller
                name="cliTelefono"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    fullWidth
                    label="Teléfono"
                    error={!!errors.cliTelefono}
                    helperText={errors.cliTelefono?.message as string}
                  />
                )}
              />
            </Grid>
            <Grid size={{ xs: 12, sm: 6 }}>
              <Controller
                name="cliEmail"
                control={control}
                render={({ field }) => (
                  <TextField
                    {...field}
                    fullWidth
                    label="Email"
                    error={!!errors.cliEmail}
                    helperText={errors.cliEmail?.message as string}
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

export default ClienteFormDialog;
