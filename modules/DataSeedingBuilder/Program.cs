using OpenModular.Module.UAP.DataSeeding;
using OpenModular.Persistence.DataSeeding.Builder;

var builder = new DataSeedingBuilder(Path.Combine(AppContext.BaseDirectory, "../../../../WebHost"));

builder.Register<UAPDataSeedingDefinition>();

builder.Build();
