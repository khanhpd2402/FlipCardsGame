using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FlipCardsGame.Models;

namespace FlipCardsGame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider serviceProvider { get; set; }
        public IConfiguration configuration { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollecction = new ServiceCollection();
            serviceCollecction.AddTransient<MainWindow>();
            serviceCollecction.AddScoped<QuizGameDBContext>();
            serviceProvider = serviceCollecction.BuildServiceProvider();
            serviceProvider.GetRequiredService<MainWindow>().Show();
        }
    }

}
