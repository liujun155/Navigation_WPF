using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Navigation_WPF
{
    public class RelayCommand : ICommand
    {
        // 执行命令的委托
        private readonly Action<object> _execute;
        // 判断命令是否可执行的委托
        private readonly Func<object, bool> _canExcute;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="execute">执行命令的委托</param>
        /// <param name="canExcute">判断命令是否可执行的委托</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RelayCommand(Action<object> execute, Func<object, bool> canExcute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExcute = canExcute;
        }

        /// <summary>
        /// ICommand接口中的CanExecuteChanged事件
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// 判断命令是否可执行
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => _canExcute == null || _canExcute(parameter);
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter) => _execute(parameter);
    }
}
