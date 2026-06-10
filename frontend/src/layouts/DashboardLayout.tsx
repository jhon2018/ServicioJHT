import { Outlet } from 'react-router-dom';
import { Box, CssBaseline, AppBar, Toolbar, Typography, Drawer, List, ListItem, ListItemButton, ListItemIcon, ListItemText } from '@mui/material';
import DashboardIcon from '@mui/icons-material/Dashboard';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import PeopleIcon from '@mui/icons-material/People';
import PersonIcon from '@mui/icons-material/Person';
import AssignmentIcon from '@mui/icons-material/Assignment';
import LogoutIcon from '@mui/icons-material/Logout';
import AssessmentIcon from '@mui/icons-material/Assessment';
import SecurityIcon from '@mui/icons-material/Security';
import NotificationsIcon from '@mui/icons-material/Notifications';
import useAuthStore from '../store/authStore';
import { useNavigate, useLocation } from 'react-router-dom';

const drawerWidth = 260;

const DashboardLayout = () => {
  const { logout, user } = useAuthStore();
  const navigate = useNavigate();
  const location = useLocation();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  const menuItems = [
    { text: 'Dashboard', icon: <DashboardIcon />, path: '/dashboard' },
    { text: 'Servicios', icon: <AssignmentIcon />, path: '/servicios' },
    { text: 'Clientes', icon: <PeopleIcon />, path: '/clientes' },
    { text: 'Conductores', icon: <PersonIcon />, path: '/conductores' },
    { text: 'Vehículos', icon: <LocalShippingIcon />, path: '/vehiculos' },
    { text: 'Auditoría', icon: <SecurityIcon />, path: '/auditoria' },
    { text: 'Reportes', icon: <AssessmentIcon />, path: '/reportes' },
  ];

  return (
    <Box sx={{ display: 'flex' }}>
      <CssBaseline />
      <AppBar 
        position="fixed" 
        elevation={0}
        sx={{ 
          width: `calc(100% - ${drawerWidth}px)`, 
          ml: `${drawerWidth}px`,
          bgcolor: 'background.paper',
          color: 'text.primary',
          borderBottom: '1px solid',
          borderColor: 'divider'
        }}
      >
        <Toolbar>
          <Typography variant="h6" noWrap component="div" sx={{ flexGrow: 1, fontWeight: 700 }}>
            {menuItems.find(i => location.pathname.startsWith(i.path))?.text || 'Dashboard'}
          </Typography>
          <NotificationsIcon sx={{ mr: 2, color: 'text.secondary' }} />
          <Typography variant="body2" sx={{ fontWeight: 'bold' }}>
            {user?.username}
          </Typography>
        </Toolbar>
      </AppBar>
      <Drawer
        variant="permanent"
        sx={{
          width: drawerWidth,
          flexShrink: 0,
          [`& .MuiDrawer-paper`]: { 
            width: drawerWidth, 
            boxSizing: 'border-box',
            bgcolor: '#1E293B', // Dark slate blue
            color: 'white',
            borderRight: 'none'
          },
        }}
      >
        <Box sx={{ p: 2, display: 'flex', alignItems: 'center', justifyContent: 'center', height: 64 }}>
          <Box component="img" src="/assets/Jht_Blanco.png" alt="JHT Logo" sx={{ height: 40 }} />
        </Box>
        <Box sx={{ overflow: 'auto', display: 'flex', flexDirection: 'column', height: '100%' }}>
          <List sx={{ flexGrow: 1, px: 2 }}>
            {menuItems.map((item) => {
              const selected = location.pathname === item.path || (item.path !== '/' && location.pathname.startsWith(item.path));
              return (
                <ListItem key={item.text} disablePadding sx={{ mb: 0.5 }}>
                  <ListItemButton 
                    onClick={() => navigate(item.path)}
                    selected={selected}
                    sx={{
                      borderRadius: 2,
                      bgcolor: selected ? 'rgba(255, 255, 255, 0.1)' : 'transparent',
                      '&:hover': { bgcolor: 'rgba(255, 255, 255, 0.15)' },
                      '&.Mui-selected': { bgcolor: 'rgba(255, 255, 255, 0.1)', '&:hover': { bgcolor: 'rgba(255, 255, 255, 0.15)' } }
                    }}
                  >
                    <ListItemIcon sx={{ color: selected ? 'white' : 'rgba(255,255,255,0.7)', minWidth: 40 }}>
                      {item.icon}
                    </ListItemIcon>
                    <ListItemText 
                      primary={item.text} 
                      slotProps={{
                        primary: {
                          sx: {
                            fontWeight: selected ? 'bold' : 'normal',
                            color: selected ? 'white' : 'rgba(255,255,255,0.7)'
                          }
                        }
                      }} 
                    />
                  </ListItemButton>
                </ListItem>
              );
            })}
          </List>
          <List sx={{ px: 2, mb: 2 }}>
             <ListItem disablePadding>
                <ListItemButton 
                  onClick={handleLogout}
                  sx={{ borderRadius: 2, '&:hover': { bgcolor: 'rgba(239, 68, 68, 0.1)' } }}
                >
                  <ListItemIcon sx={{ minWidth: 40 }}><LogoutIcon sx={{ color: '#ef4444' }} /></ListItemIcon>
                  <ListItemText 
                    primary="Cerrar Sesión" 
                    slotProps={{ primary: { sx: { color: '#ef4444', fontWeight: 'bold' } } }} 
                  />
                </ListItemButton>
              </ListItem>
          </List>
        </Box>
      </Drawer>
      <Box component="main" sx={{ flexGrow: 1, p: 4, backgroundColor: '#F8FAFC', minHeight: '100vh' }}>
        <Toolbar />
        <Outlet />
      </Box>
    </Box>
  );
};

export default DashboardLayout;
