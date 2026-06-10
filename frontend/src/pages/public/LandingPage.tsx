import { Box, Button, Container, Typography, Grid, Card, CardContent, IconButton, useTheme, AppBar, Toolbar, Dialog, DialogTitle, DialogContent, DialogContentText, TextField, DialogActions } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';
import SecurityIcon from '@mui/icons-material/Security';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import LocationOnIcon from '@mui/icons-material/LocationOn';
import SearchIcon from '@mui/icons-material/Search';

const LandingPage = () => {
  const navigate = useNavigate();
  const theme = useTheme();
  
  // Tracking Modal State
  const [openTracking, setOpenTracking] = useState(false);
  const [trackingCode, setTrackingCode] = useState('');

  const handleOpenTracking = () => setOpenTracking(true);
  const handleCloseTracking = () => {
    setOpenTracking(false);
    setTrackingCode('');
  };

  const handleSearchTracking = () => {
    if (trackingCode.trim()) {
      navigate(`/tracking/${trackingCode.trim()}`);
    }
  };

  return (
    <Box sx={{ minHeight: '100vh', display: 'flex', flexDirection: 'column' }}>
      {/* AppBar / Navigation */}
      <AppBar position="sticky" color="inherit" elevation={1}>
        <Toolbar>
          <Box sx={{ flexGrow: 1, display: 'flex', alignItems: 'center' }}>
            <Box component="img" src="/assets/Jht_Azul.png" alt="JHT Logo" sx={{ height: 40, mr: 2 }} />
          </Box>
          <Button color="inherit" onClick={() => document.getElementById('servicios')?.scrollIntoView({ behavior: 'smooth' })}>
            Servicios
          </Button>
          <Button color="inherit" onClick={() => document.getElementById('nosotros')?.scrollIntoView({ behavior: 'smooth' })}>
            Nosotros
          </Button>
          <Button 
            variant="outlined" 
            color="primary" 
            sx={{ ml: 2, mr: 1 }}
            onClick={handleOpenTracking}
            startIcon={<SearchIcon />}
          >
            Tracking
          </Button>
          <Button 
            variant="contained" 
            color="primary"
            onClick={() => navigate('/login')}
          >
            Acceso
          </Button>
        </Toolbar>
      </AppBar>

      {/* Hero Section */}
      <Box 
        sx={{
          position: 'relative',
          height: '70vh',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          color: 'white',
          backgroundImage: 'url(/assets/bannerJHT.png)',
          backgroundSize: 'cover',
          backgroundPosition: 'center',
          '&::before': {
            content: '""',
            position: 'absolute',
            top: 0,
            left: 0,
            right: 0,
            bottom: 0,
            backgroundColor: 'rgba(0, 51, 102, 0.7)', // Azul Corporativo Overlay
          }
        }}
      >
        <Container maxWidth="md" sx={{ position: 'relative', zIndex: 1, textAlign: 'center' }}>
          <Typography variant="h2" component="h1" sx={{ fontWeight: 'bold', mb: 2 }}>
            Conectando tu negocio con el éxito
          </Typography>
          <Typography variant="h5" sx={{ mb: 4, fontWeight: 300 }}>
            Soluciones logísticas integrales en transporte terrestre, a tiempo y seguros.
          </Typography>
          <Button 
            variant="contained" 
            color="secondary" 
            size="large" 
            onClick={handleOpenTracking}
            sx={{ py: 1.5, px: 4, fontSize: '1.1rem' }}
          >
            Rastrea tu carga ahora
          </Button>
        </Container>
      </Box>

      {/* Ventajas Competitivas */}
      <Box sx={{ py: 8, bgcolor: 'background.default' }}>
        <Container maxWidth="lg">
          <Grid container spacing={4}>
            {[
              { icon: <LocationOnIcon fontSize="large" color="primary" />, title: 'Trazabilidad 24/7', desc: 'Seguimiento en tiempo real de tu mercancía.' },
              { icon: <LocalShippingIcon fontSize="large" color="primary" />, title: 'Cobertura Nacional', desc: 'Llegamos a todos los destinos del país de forma segura.' },
              { icon: <SecurityIcon fontSize="large" color="primary" />, title: 'Seguridad Garantizada', desc: 'Monitoreo constante y estrictos protocolos de protección.' }
            ].map((feature, index) => (
              <Grid size={{ xs: 12, md: 4 }} key={index}>
                <Card 
                  elevation={0} 
                  sx={{ 
                    height: '100%', 
                    textAlign: 'center', 
                    p: 3, 
                    border: `1px solid ${theme.palette.divider}`,
                    transition: 'transform 0.3s ease-in-out',
                    '&:hover': { transform: 'translateY(-10px)', boxShadow: theme.shadows[4] }
                  }}
                >
                  <CardContent>
                    <IconButton sx={{ mb: 2, pointerEvents: 'none' }}>
                      {feature.icon}
                    </IconButton>
                    <Typography variant="h6" sx={{ fontWeight: 'bold', mb: 1 }}>
                      {feature.title}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                      {feature.desc}
                    </Typography>
                  </CardContent>
                </Card>
              </Grid>
            ))}
          </Grid>
        </Container>
      </Box>

      {/* Servicios y Nosotros */}
      <Box id="servicios" sx={{ py: 8, bgcolor: '#f8f9fa' }}>
        <Container maxWidth="lg">
          <Grid container spacing={6} sx={{ alignItems: 'center' }}>
            <Grid size={{ xs: 12, md: 6 }}>
              <Box 
                component="img"
                src="/assets/services_truck_01.png"
                alt="Operación Logística"
                sx={{ width: '100%', borderRadius: 2, boxShadow: theme.shadows[4] }}
              />
            </Grid>
            <Grid size={{ xs: 12, md: 6 }} id="nosotros">
              <Typography variant="h4" sx={{ fontWeight: 'bold', mb: 2 }} color="primary">
                Excelencia en cada entrega
              </Typography>
              <Typography variant="body1" color="text.secondary" sx={{ mb: 3 }}>
                En JHT Logistics, combinamos tecnología de vanguardia con años de experiencia en el sector transporte. 
                Nuestra misión es simplificar tu cadena de suministro, ofreciendo carga completa, paquetería y distribución nacional con los más altos estándares de calidad.
              </Typography>
              <Button variant="outlined" color="primary" size="large" onClick={() => navigate('/login')}>
                Portal Administrativo
              </Button>
            </Grid>
          </Grid>
        </Container>
      </Box>

      {/* Footer */}
      <Box sx={{ bgcolor: theme.palette.primary.main, color: 'white', py: 4, mt: 'auto' }}>
        <Container maxWidth="lg" sx={{ textAlign: 'center' }}>
          <Typography variant="h6" sx={{ fontWeight: 'bold', mb: 1 }}>
            JHT Logistics S.A.C.
          </Typography>
          <Typography variant="body2" sx={{ opacity: 0.8, mb: 2 }}>
            El aliado estratégico para tu cadena de suministro.
          </Typography>
          <Typography variant="caption" sx={{ opacity: 0.6 }}>
            &copy; {new Date().getFullYear()} Todos los derechos reservados.
          </Typography>
        </Container>
      </Box>

      {/* Tracking Dialog */}
      <Dialog open={openTracking} onClose={handleCloseTracking}>
        <DialogTitle>Rastrear Servicio</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Por favor, ingresa el código de rastreo (Token) proporcionado por tu ejecutivo de cuenta.
          </DialogContentText>
          <TextField
            autoFocus
            margin="dense"
            label="Código de Rastreo"
            type="text"
            fullWidth
            variant="outlined"
            value={trackingCode}
            onChange={(e) => setTrackingCode(e.target.value)}
            onKeyPress={(e) => {
              if (e.key === 'Enter') {
                handleSearchTracking();
              }
            }}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCloseTracking}>Cancelar</Button>
          <Button onClick={handleSearchTracking} variant="contained" disabled={!trackingCode.trim()}>
            Buscar
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
};

export default LandingPage;
