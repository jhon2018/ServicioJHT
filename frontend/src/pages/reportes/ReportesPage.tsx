import { Box, Card, CardContent, Typography, Grid } from '@mui/material';

const ReportesPage = () => {
  return (
    <Box>
      <Typography variant="h4" sx={{ fontWeight: 'bold', mb: 3 }}>
        Reportes y Analítica
      </Typography>
      <Grid container spacing={3}>
        <Grid size={{ xs: 12, sm: 6, md: 4 }}>
          <Card>
            <CardContent>
              <Typography variant="h6">Servicios por Estado</Typography>
              <Typography variant="body2" color="text.secondary" sx={{ mt: 1 }}>
                Gráfico de distribución (Pendiente de implementación de Chart.js)
              </Typography>
            </CardContent>
          </Card>
        </Grid>
        <Grid size={{ xs: 12, sm: 6, md: 4 }}>
          <Card>
            <CardContent>
              <Typography variant="h6">Eficiencia de Entregas</Typography>
              <Typography variant="body2" color="text.secondary" sx={{ mt: 1 }}>
                Métrica On-Time Delivery (Pendiente)
              </Typography>
            </CardContent>
          </Card>
        </Grid>
      </Grid>
    </Box>
  );
};

export default ReportesPage;
