namespace AbyssiniaPocketLaw.API.Persistance;

public class RemoteServerSetting
{
    public string? SSHServer { get; set; }
    public string? SSHUserName { get; set; }
    public string? SSHPassword { get; set; }
    public string? DatabaseServer { get; set; }
    public string? DatabaseUserName { get; set; }
    public string? databasePassword { get; set; }
    public string? DatabaseName { get; set; }
    public string? LocalIP { get; set; }
    public bool ShouldConnectSSH { get; set; }
}
