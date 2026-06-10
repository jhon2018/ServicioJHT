import { Navigate, Outlet } from 'react-router-dom';
import useAuthStore from '../store/authStore';

interface AuthGuardProps {
  allowedRoles?: string[];
}

const AuthGuard = ({ allowedRoles }: AuthGuardProps) => {
  const { isAuthenticated, user } = useAuthStore();

  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }

  if (allowedRoles && user) {
    const hasRole = user.roles.some((role) => allowedRoles.includes(role));
    if (!hasRole) {
      return <Navigate to="/unauthorized" replace />;
    }
  }

  return <Outlet />;
};

export default AuthGuard;
