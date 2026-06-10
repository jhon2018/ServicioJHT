import { Outlet } from 'react-router-dom';
import { Box, AppBar, Toolbar, Typography, Container } from '@mui/material';

const PublicLayout = () => {
  return (
    <Box sx={{ display: 'flex', flexDirection: 'column', minHeight: '100vh', bgcolor: 'background.default' }}>
      <AppBar position="static" color="primary" elevation={0}>
        <Toolbar>
          <Container maxWidth="md">
            <Typography variant="h6" sx={{ fontWeight: 700 }}>
              JHT Logistics Tracking
            </Typography>
          </Container>
        </Toolbar>
      </AppBar>
      <Box component="main" sx={{ flexGrow: 1, py: 4 }}>
        <Container maxWidth="md">
          <Outlet />
        </Container>
      </Box>
      <Box component="footer" sx={{ py: 3, textAlign: 'center', bgcolor: 'white', borderTop: '1px solid #eee' }}>
        <Typography variant="body2" color="text.secondary">
          © {new Date().getFullYear()} JHT Transport Company S.A.C.
        </Typography>
      </Box>
    </Box>
  );
};

export default PublicLayout;
