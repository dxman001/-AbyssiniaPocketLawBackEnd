namespace AbyssiniaPocketLaw.API.Persistance;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Renci.SshNet;
using ConnectionInfo = Renci.SshNet.ConnectionInfo;

public static class PersistanceServiceCollection
{
    public static IServiceCollection AddPocketLawDbContext(this IServiceCollection services, IConfiguration Configuration)
    {
        var remoteServerSetting = Configuration
               .GetSection("RemoteServerSetting")
               .Get<RemoteServerSetting>();

        if (remoteServerSetting.ShouldConnectSSH)
        {
            var (sshClient, localPort) = ConnectSsh(remoteServerSetting.SSHServer, remoteServerSetting.SSHUserName, remoteServerSetting.SSHPassword, databaseServer: remoteServerSetting.DatabaseServer);
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
            {
                Server = remoteServerSetting.LocalIP,
                Port = localPort,
                UserID = remoteServerSetting.DatabaseUserName,
                Password = remoteServerSetting.databasePassword,
                Database = remoteServerSetting.DatabaseName,
            };

            services.AddDbContext<PocketLawDbContext>(
             opt =>
                 opt.UseMySql(builder.ConnectionString, ServerVersion.Parse("5.7.40-mysql")
             ));
        }
        else
        {
            services.AddDbContext<PocketLawDbContext>(
            opt =>
                opt.UseMySql(Configuration.GetConnectionString("PocketLawDB"), ServerVersion.Parse("8.0.30-mysql")
            ));
        }

        return services;
    }

    private static (SshClient SshClient, uint Port) ConnectSsh(string? sshHostName, string? sshUserName, string? sshPassword = null,
    string? sshKeyFile = null, string? sshPassPhrase = null, int sshPort = 22, string? databaseServer = "localhost", int databasePort = 3306)
    {
        
        if (string.IsNullOrEmpty(sshHostName))
            throw new ArgumentException($"{nameof(sshHostName)} must be specified.", nameof(sshHostName));
        if (string.IsNullOrEmpty(sshHostName))
            throw new ArgumentException($"{nameof(sshUserName)} must be specified.", nameof(sshUserName));
        if (string.IsNullOrEmpty(sshPassword) && string.IsNullOrEmpty(sshKeyFile))
            throw new ArgumentException($"One of {nameof(sshPassword)} and {nameof(sshKeyFile)} must be specified.");
        if (string.IsNullOrEmpty(databaseServer))
            throw new ArgumentException($"{nameof(databaseServer)} must be specified.", nameof(databaseServer));

       
        var authenticationMethods = new List<AuthenticationMethod>();
        if (!string.IsNullOrEmpty(sshKeyFile))
        {
            authenticationMethods.Add(new PrivateKeyAuthenticationMethod(sshUserName,
                new PrivateKeyFile(sshKeyFile, string.IsNullOrEmpty(sshPassPhrase) ? null : sshPassPhrase)));
        }
        if (!string.IsNullOrEmpty(sshPassword))
        {
            authenticationMethods.Add(new PasswordAuthenticationMethod(sshUserName, sshPassword));
        }

        
        var sshClient = new SshClient(new ConnectionInfo(sshHostName, sshPort, sshUserName, authenticationMethods.ToArray()));
        sshClient.Connect();

       
        var forwardedPort = new ForwardedPortLocal("127.0.0.1", databaseServer, (uint)databasePort);
        sshClient.AddForwardedPort(forwardedPort);
        forwardedPort.Start();

        return (sshClient, forwardedPort.BoundPort);
    }
}
