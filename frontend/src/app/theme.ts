import { createTheme } from '@mui/material/styles';

const theme = createTheme({
  palette: {
    primary: {
      main: '#0D47A1', // Azul corporativo genérico (debe ajustarse al real)
      contrastText: '#ffffff',
    },
    secondary: {
      main: '#ffffff', // Blanco
      contrastText: '#0D47A1',
    },
    background: {
      default: '#f5f5f5', // Gris claro para fondo
      paper: '#ffffff',
    },
    success: {
      main: '#2e7d32', // Verde
    },
    warning: {
      main: '#ed6c02', // Amarillo/Naranja
    },
    info: {
      main: '#0288d1', // Celeste
    },
    error: {
      main: '#d32f2f', // Rojo
    },
  },
  typography: {
    fontFamily: '"Inter", "Roboto", "Helvetica", "Arial", sans-serif',
    button: {
      textTransform: 'none', // Botones más limpios y modernos
      fontWeight: 600,
    },
  },
  components: {
    MuiButton: {
      styleOverrides: {
        root: {
          borderRadius: 8,
          boxShadow: 'none',
          '&:hover': {
            boxShadow: 'none',
          },
        },
      },
    },
    MuiCard: {
      styleOverrides: {
        root: {
          borderRadius: 12,
          boxShadow: '0px 4px 20px rgba(0, 0, 0, 0.05)',
        },
      },
    },
    MuiTextField: {
      styleOverrides: {
        root: {
          '& .MuiOutlinedInput-root': {
            borderRadius: 8,
          },
        },
      },
    },
  },
});

export default theme;
