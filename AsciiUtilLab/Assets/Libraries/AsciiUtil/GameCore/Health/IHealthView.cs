namespace AsciiUtil.UI
{
    public interface IHealthView
    {
        /// <summary>
        /// 初期値を設定します
        /// </summary>
        /// <param name="value"></param> <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void Initialize(float value);
        /// <summary>
        /// 値を更新します
        /// </summary>
        /// <param name="value"></param>
        public void UpdateValue(float value);
    }
}