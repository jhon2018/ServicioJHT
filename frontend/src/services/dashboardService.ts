import api from './api';

export interface DashboardMetrics {
  serviciosActivos: number;
  unidadesEnRuta: number;
  entregasHoy: number;
}

export const getDashboardMetrics = async (): Promise<DashboardMetrics> => {
  const response = await api.get<DashboardMetrics>('/Dashboard/metrics');
  return response.data;
};
