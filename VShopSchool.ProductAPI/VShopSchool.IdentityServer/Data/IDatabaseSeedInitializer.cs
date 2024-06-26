namespace VShopSchool.IdentityServer.Data
{
    public interface IDatabaseSeedInitializer
    {
        void InitializeSeedRoles();
        void InitializeSeedUsers();
    }
}
