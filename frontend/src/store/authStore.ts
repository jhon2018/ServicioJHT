import { create } from 'zustand';
import { persist } from 'zustand/middleware';

interface AuthState {
  token: string | null;
  isAuthenticated: boolean;
  user: {
    username: string;
    roles: string[];
  } | null;
  login: (token: string, username: string, roles: string[]) => void;
  logout: () => void;
}

const useAuthStore = create<AuthState>()(
  persist(
    (set) => ({
      token: null,
      isAuthenticated: false,
      user: null,
      login: (token, username, roles) =>
        set({ token, isAuthenticated: true, user: { username, roles } }),
      logout: () => set({ token: null, isAuthenticated: false, user: null }),
    }),
    {
      name: 'auth-storage', // Nombre para el localStorage
    }
  )
);

export default useAuthStore;
