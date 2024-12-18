﻿## Lab2

# Варіант 7

Карта лабіринту є квадратне поле розміром N×N. Деякі квадрати цього поля заборонені для проходження. Крок у лабіринті – переміщення з однієї дозволеної клітини до іншої дозволеної клітини, суміжної з першою стороною. Шлях – це певна послідовність таких кроків. При цьому кожну клітину, включаючи початкову та кінцеву, можна відвідувати кілька разів.
Потрібно написати програму, яка підрахує кількість різних шляхів із клітини (1, 1) у клітину (N, N) рівно за K кроків (тобто опинитися у клітині (N, N) після K-го кроку).

## Вхідні дані
Вхідний файл INPUT.TXT містить у першому рядку числа N і K, розділені пробілом (1 < N ≤ 15, 0 < K ≤ 30). Наступні N рядків, N символів у кожному, містять карту лабіринту, починаючи з клітини (1, 1). Символ "0" означає не заборонену для проходження клітину, а символ "1" - заборонену. Початкова та кінцева клітини завжди дозволені для проходження.

## Вихідні дані

Вихідний файл OUTPUT.TXT повинен містити кількість можливих різних шляхів довжини K. У всіх тестах це значення не перевищуватиме 2147483647.

## Приклади

| №  | INPUT.TXT        | OUTPUT.TXT  |
|----|------------------|-------------|
| 1  |3 6 <br> 000 <br> 101 <br> 100| 5  |
| 2  |2 8 <br> 01 <br> 10| 0 |

### Запуск програми
```bash
dotnet run --project App
```
### Запуск тестів
Без проміжних результатів
```bash
dotnet test
```
З проміжними результатами
```bash
dotnet test --logger "console;verbosity=detailed"
```

### Результати

Вхідні дані беруться з файлу `INPUT.TXT`, результати записуються в файл `OUTPUT.TXT`


### Алгоритм розв'язку
1. Валідація вхідних даних
Спочатку перевіряється, чи вхідні дані не є порожніми або складаються лише з пробілів.
Далі вхідні дані розбираються на gridSize (розмір лабіринту), steps (точна кількість кроків) та саму карту лабіринту (grid).
Виконується перевірка:
- Чи коректний розмір лабіринту (gridSize > 0).
- Чи кількість кроків (steps) не є негативною.
- Чи містить лабіринт тільки допустимі символи ('0' або '1'), щоб виключити помилки в форматі.

2. Розбір вхідних даних
Перший рядок вхідного файлу містить два числа — розмір лабіринту (N) і кількість кроків (K).
Наступні N рядків містять карту лабіринту у вигляді матриці N x N.
Лабіринт зберігається у вигляді двовимірного масиву grid, де кожен елемент вказує на те, чи є клітинка прохідною ('0') або заблокованою ('1').

3. Динамічне програмування для обчислення шляхів
Використовується тривимірний масив paths для зберігання кількості шляхів, які можуть привести до кожної клітинки лабіринту за певну кількість кроків.
paths[k % 2, i, j] зберігає кількість способів дійти до клітинки (i, j) за k кроків.
Для оптимізації пам'яті зберігаються лише два останні рівні k % 2, оскільки для обчислення кількості шляхів на кожному новому кроці необхідно мати доступ тільки до даних попереднього кроку.

4. Ініціалізація
Встановлюється початкове значення для paths: шляхів до (1,1) за нуль кроків є 1 (paths[0, 1, 1] = 1), адже ми починаємо саме з цієї клітинки.
Всі інші клітинки для нульового кроку встановлюються в 0, оскільки ми не можемо потрапити до них безпосередньо.

5. Обчислення шляхів на кожному кроці
Цикл обробляє кожен крок від 1 до K. На кожному кроці виконується:
Скидання кількості шляхів для поточного кроку, щоб запобігти переносу даних з попередніх обчислень.
Перебір всіх прохідних клітинок у лабіринті.
Для кожної прохідної клітинки (i, j) обчислюється кількість способів потрапити до неї з сусідніх клітинок (вгору, вниз, вліво, вправо) на попередньому кроці (k-1).
Якщо клітинка дозволена для проходу (grid[i - 1, j - 1] == '0'), то її значення в paths для поточного кроку обчислюється як сума кількості шляхів із сусідніх клітинок на попередньому кроці.

6. Отримання результату
Після завершення циклу, шуканий результат — це значення paths[K % 2, N, N], яке вказує на кількість шляхів до клітинки (N, N) за рівно K кроків.
Якщо неможливо досягти (N, N) за K кроків, результат буде 0.