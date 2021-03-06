# Дипломная работа

## "Расширенное подмножество ЯП JavaScript"

### Вводная информация

За основу был взят стандарт [ECMA-262](https://www.ecma-international.org/publications-and-standards/standards/ecma-262/)

[Лексическая структура](Interpreter/tokenTypes.json)

[Грамматика](Interpreter/grammar.txt)

[Рабочие примеры](samples)

### Конструкции языка

#### Типизация
В языке структурная статическая сильная типизация.

Есть 5 примитивных типов:
1. number
2. boolean
3. string
4. null
5. void

Остальные типы делятся на группы:
- NullableType (тип, который допускает значение ```null```)
- ObjectType (тип объекта, является NullableType)
- ArrayType (списковый тип)
- FunctionType (тип функции)

##### Значения по умолчанию

| Тип      | Значение |
| ----------- | ----------- |
| number      | 0       |
| boolean   | false        |
|string| ""|
|NullableType|null|
|ArrayType|[]|
##### type alias
Можно создать свой type alias по типу того, как это сделано в С++

```
type int = number
type maybeInt = int?
type ints = int[]
type point = {
    x: int;
    y: int;
}
type handleInts = (ints) => void
type handler = {
    items: ints;
    handle: handleInts;
}
```
#### Объявление переменных
```
let i = 1 // интерпретатор выведет тип из выражения
let j: number // запишет значение по умолчанию в переменную
let k: number = 1 // полностью явное объявление
```
#### Объекты
```
let v2d = {
    // обычное поле
    x: 3;
    y: 4;
    //метод
    lengthSquared => () {
        // в методе доступны поля объекта
        // и указатель this
        return x * x + y * y
    };
}
```
#### Списки
```
let array = [1, 2, 3]
let size = ~array // длина списка
array::1 // удаление элемента по индексу
array = array ++ [5, 7] // конкатенация списков
```
#### Операторы
|Оператор|Вид|Типы операндов|Тип операции|
|---|---|---|---|
|+|бинарный|оба number, оба string|number, string
|*, -, /, %| бинарный|number|number
|&#124;&#124;, &&  |бинарный|boolean|boolean
|!=, ==|бинарный|равный с двух сторон|boolean
|<=, >=, >, <|бинарный|number|boolean
|!|унарный|boolean|boolean
|-|унарный|number|number
|++|бинарный|[]|[]
|::|бинарный|[] и number|void
|~|унарный|[]|number

#### Ветвление
```
if (1 == 1) {
    // ...
} else if (2 == 2) {
    // ...
}
else {
    // ...
}
// в общем как в Си подобных языках
// главное, чтобы выражение условия
// возвращало boolean
```
Также есть тернарный оператор
```
let x = 1 > 0 ? 0 <= 1 ? 1 : 0 : -2 < 0 ? -1 : 0
```
#### Цикл
```
while (cond) {
    // ...
    continue
    // ...
    break
}
```
#### Функции
```
// объявление
function add(a: number, b: number): number {
    return a + b
}
// вызов
let c = add(1, 2)
```
#### Операции доступа
```
// объекты
let x = v2d.x
let s = v2d.lengthSquared()
// массивы
let l = array[2]
```
#### Приведение типов
```
let s = v2d as string
```
#### Стандартная библиотека
- Функция `print` c сигнатурой `(string) => void`; осуществляет печать строки на экран

### Требования

- .NET 5 SDK, dot

### Сборка
После клонирования репозитория идём в папку проекта `Interpreter`.

Там выполняем команду:
```dotnet publish -r <RUNTIME_IDENTIFIER> -p:PublishSingleFile=true -p:DebugType=embedded --self-contained false -o <OUTPUT_DIRECTORY>```

Список идентификаторов рантайма лежит [тут](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog#windows-rids)

### Запуск

```
Interpreter 1.0.0
Copyright (C) 2022 Interpreter

ERROR(S):
  Option 'h' is unknown.
  Required option 'i' is missing.

  -t           (Default: tokenTypes.json) Path to lexer configuration

  -d           (Default: false) Show dump data of interpreter

  -i           Required. Path to input file

  --help       Display this help screen.

  --version    Display version information.
```

### Источники:

1. [ECMA-262](https://www.ecma-international.org/publications-and-standards/standards/ecma-262/)
2. [DragonBook](https://suif.stanford.edu/dragonbook/)
3. [Stanford CS143 Lectures](https://web.stanford.edu/class/archive/cs/cs143/cs143.1128/)