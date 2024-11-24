using Hangfire;

namespace Locadora.Veiculo.Services.Jobs;

public static class JobsConfig
{
    #region Jobs Ids
    private const string IniciarManutencoesProgramadas = "inicar-manutencoes-programadas";
    #endregion
    
    #region Criar
    public static void Criar()
    {
        RecurringJob.AddOrUpdate<IManutencaoJob>(
            IniciarManutencoesProgramadas,
            x => x.IniciarManutencoesProgramadasAsync(),
            TodoDiaAs(02, 29),
            ObterOpcoes());
    }
    #endregion
    
    #region Utils
    
    #region ACadaMinuto
    private static Func<string> ACadaMinuto()
    {
        return Cron.Minutely;
    }    
    #endregion
    
    #region ACadaHora
    private static Func<string> ACadaHora()
    {
        return Cron.Hourly;
    }
    #endregion
    
    #region ACadaDia
    private static Func<string> ACadaDia()
    {
        return Cron.Daily;
    }
    #endregion
    
    #region ACadaSemana
    private static Func<string> ACadaSemana()
    {
        return Cron.Weekly;
    }
    #endregion
    
    #region ACadaMes
    private static Func<string> ACadaMes()
    {
        return Cron.Monthly;
    }
    #endregion
    
    #region ACadaAno
    private static Func<string> ACadaAno()
    {
        return Cron.Yearly;
    }
    #endregion
    
    #region TodoDiaAs
    private static Func<string> TodoDiaAs(int hora, int minuto)
    {
        return () => $"{minuto} {hora} * * *";
    }
    #endregion
    
    #region ObterOpcoes
    private static RecurringJobOptions ObterOpcoes()
    {
        return new RecurringJobOptions
        {
            TimeZone = TimeZoneInfo.Local
        };
    }
    #endregion
    #endregion
}