namespace OpenModular.Persistence.DataSeeding
{
    public class DataSeedingRecord
    {
        /// <summary>
        /// 所属模块
        /// </summary>
        public string Module { get; }

        /// <summary>
        /// 实体名称
        /// </summary>
        public string EntityName { get; }

        /// <summary>
        /// 模式
        /// </summary>
        public DataSeedingMode Mode { get; }

        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// SQL模式
        /// </summary>
        public DataSeedingSqlMode SqlMode { get; set; }

        public DataSeedingRecord(string module, string entityName, DataSeedingMode mode, string data, DataSeedingSqlMode sqlMode = DataSeedingSqlMode.Common)
        {
            Module = module;
            EntityName = entityName;
            Mode = mode;
            Data = data;
            SqlMode = sqlMode;
        }
    }
}
