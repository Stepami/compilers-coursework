using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Interpreter.Lib.Semantic.Symbols;
using Type = Interpreter.Lib.Semantic.Types.Type;

namespace Interpreter.Lib.Semantic
{
    public class SymbolTable : IDisposable
    {
        private readonly Dictionary<string, Symbol> _symbols = new();
        private readonly Dictionary<string, Type> _types = new();
        
        private SymbolTable _openScope;
        private readonly List<SymbolTable> _subScopes = new();

        public void AddOpenScope(SymbolTable table)
        {
            _openScope = table;
            table._subScopes.Add(this);
        }

        public void AddSymbol(Symbol symbol) => _symbols[symbol.Id] = symbol;

        public void AddType(Type type, string typeId = null) =>
            _types[typeId ?? type.ToString()] = type;
        
        public Type FindType(string typeId)
        {
            var hasInsideTheScope = _types.TryGetValue(typeId, out var type);
            return !hasInsideTheScope ? _openScope?.FindType(typeId) : type;
        }

        /// <summary>
        /// Поиск эффективного символа
        /// </summary>
        public T FindSymbol<T>(string id) where T : Symbol
        {
            var hasInsideTheScope = _symbols.TryGetValue(id, out var symbol);
            return !hasInsideTheScope ? _openScope?.FindSymbol<T>(id) : symbol as T;
        }

        /// <summary>
        /// Проверяет наличие собственного символа
        /// </summary>
        public bool ContainsSymbol(string id) => _symbols.ContainsKey(id);

        public void Clear() => _symbols.Clear();

        private void DeepClean()
        {
            Clear();
            foreach (var child in _subScopes)
            {
                child.DeepClean();
            }
        }

        [SuppressMessage("ReSharper", "CA1816")]
        public void Dispose()
        {
            var table = _openScope;
            while (table._openScope != null)
            {
                table = table._openScope;
            }

            table.DeepClean();
        }
    }
}