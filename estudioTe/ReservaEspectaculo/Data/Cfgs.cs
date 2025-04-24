namespace ReservaEspectaculo.Data
{
    public static class Cfgs
    {
        //Limites
        public const int NomAppMin = 2;
        public const int NomAppMax = 40;
        public const int DniMin = 1000000;
        public const int DniMax = 99999999;
        public const long TelefonoMin = 1000000;
        public const long TelefonoMax = 999999999999;
        public const int DireccionMax = 120;
        public const int ButacasReservaMin = 1;
        public const int LegajoMin = 1;
        public const int LegajoMax = 99999999;
        public const int NomGeneroMin = 2;
        public const int NomGeneroMax = 50;
        public const int NombreRolMin = 3;
        public const int NombreRolMax = 30;
        public const int UsuarioNombreMin = 2;
        public const int UsuarioNombreMax = 50;
        public const int UsuarioMailMin = 2;
        public const int UsuarioMailMax = 50;
        public const int UsuarioPasswordMin = 6;
        public const int UsuarioPasswordMax = 50;
        public const int TituloMin = 1;
        public const int TituloMax = 50;
        public const int DescripcionMin = 2;
        public const int DescripcionMax = 200;
        public const int CapacidadButacasMin = 10;
        public const int CapacidadButacasMax = 200;
        //Usuarios
        public const string PassComun = "Password1!";
        public const string UsuarioAdmin = "admin@admin.com";
        public const string UsuarioEmpleado = "empleado@empleado.com";
        public const string UsuarioCliente = "cliente@cliente.com";
        //Roles
        public const string RolAdmin = "Admin";
        public const string RolEmpleado = "Empleado";
        public const string RolCliente = "Cliente";

    }
}
