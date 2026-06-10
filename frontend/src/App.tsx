import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { ThemeProvider } from '@mui/material/styles';
import { CssBaseline } from '@mui/material';
import theme from './app/theme';

// Layouts
import DashboardLayout from './layouts/DashboardLayout';
import PublicLayout from './layouts/PublicLayout';
import AuthGuard from './components/AuthGuard';

import LandingPage from './pages/public/LandingPage';
import LoginPage from './pages/LoginPage';
import DashboardPage from './pages/DashboardPage';
import TrackingPage from './pages/TrackingPage';
import ClientesPage from './pages/clientes/ClientesPage';
import ConductoresPage from './pages/conductores/ConductoresPage';
import VehiculosPage from './pages/vehiculos/VehiculosPage';
import ServiciosPage from './pages/servicios/ServiciosPage';
import ServicioDetallePage from './pages/servicios/ServicioDetallePage';
import AuditoriaPage from './pages/auditoria/AuditoriaPage';
import ReportesPage from './pages/reportes/ReportesPage';

// Placeholder Pages for menus
const UnauthorizedPage = () => <div>Acceso Denegado</div>;

function App() {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Router>
        <Routes>
          {/* Rutas Públicas */}
          <Route path="/" element={<LandingPage />} />
          <Route path="/login" element={<LoginPage />} />
          
          <Route element={<PublicLayout />}>
            <Route path="/tracking/:token" element={<TrackingPage />} />
          </Route>

          {/* Rutas Protegidas (Dashboard) */}
          <Route element={<AuthGuard />}>
            <Route element={<DashboardLayout />}>
              <Route path="/dashboard" element={<DashboardPage />} />
              <Route path="/clientes" element={<ClientesPage />} />
              <Route path="/conductores" element={<ConductoresPage />} />
              <Route path="/vehiculos" element={<VehiculosPage />} />
              <Route path="/servicios" element={<ServiciosPage />} />
              <Route path="/servicios/:id" element={<ServicioDetallePage />} />
              <Route path="/auditoria" element={<AuditoriaPage />} />
              <Route path="/reportes" element={<ReportesPage />} />
            </Route>
          </Route>

          {/* Fallbacks */}
          <Route path="/unauthorized" element={<UnauthorizedPage />} />
          <Route path="*" element={<Navigate to="/" replace />} />
        </Routes>
      </Router>
    </ThemeProvider>
  );
}

export default App;
