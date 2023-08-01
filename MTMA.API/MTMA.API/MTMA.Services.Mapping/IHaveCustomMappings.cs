namespace MTMA.Services.Mapping
{
    using AutoMapper;

    /// <summary>
    /// Defines an interface for custom mappings using AutoMapper.
    /// Classes that implement this interface can provide their own custom mapping logic.
    /// </summary>
    public interface IHaveCustomMappings
    {
        /// <summary>
        /// Create custom mappings between types using AutoMapper.
        /// </summary>
        /// <param name="configuration">The AutoMapper profile configuration to define mappings.</param>
        void CreateMappings(IProfileExpression configuration);
    }
}
