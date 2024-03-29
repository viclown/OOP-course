# Объектно-ориентированное программирование, 3 семестр

## Лабораторная работа 0. Isu
**Цели:** 
1. Знакомство с языком `C#`
2. Знакомство с базовыми механизмами ООП
3. Работа с unit-тестами

**Предметная область:** студенты, группы, переводы, поиск. Система должна поддерживать 
1. Механизм перевода между группами, 
2. Механизм добавления в группу
3. Механизм удаления из группы.
4. Создание студента

## Лабораторная работа 1. Shops
**Цели:** 
1. Демонстрация умения выделять сущности и проектировать по ним классы.
2. Переопределение методов

**Предметная область:** магазин, покупатель, поставка, пополнение и покупка товаров. Необходимо реализовать
1. Покупку товаров в магазине 
2. Покупку партии товаров 
3. Поставки товаров в магазин
4. Поиск магазина, в котором товар (или партию товаров) можно купить дешевле
5. Установка и изменение цен в магазине
6. Получение магазином денег, снятие денег со счета покупателя

## Лабораторная работа 2. ISUExtra (<3 ОГНП)
Дополнение к 0 лабораторной работе. 

**Цели:** 
1. Научиться выделять зоны ответственности разных сущностей и проектировать связи между ними. 
2. Работа с наследованием

**Предметная область:** реализация системы записи студентов на курсы по выбору.

## Лабораторная работа 3. Backups
**Цели:** работа с файловой и виртуальной системами, применение на практике принципы из `SOLID`, `GRASP`, изучение и применение паттерна `Strategy`

**Предметная область:** `Backup`, `Restore Point`, `Backup Job`, `Job Object`, `Storage`, `Repository`

## Лабораторная работа 4. Banks
**Цели:** 
1. Изучение и применение на практике принципы из `SOLID`, `GRASP`
2. Изучение и применение паттернов `Builder`, `Observer`, `Strategy`, `Singleton`, `Command`
3. Разработка консольного интерфейса для пользователей.

**Предметная область:** 
Банки и Центральный Банк, клиенты, счета и проценты, комиссии, операции и транзакции, обновление условий счета, консольный интерфейс.

## Лабораторная работа 5. BackupsExtra
**Цели:** 
1. Применение логирования 
2. Реализация сохранения и загрузки данных

**Предметная область:** та же, что и в лабораторной работе 3 + `Merge`.
1. Система должна уметь загружать свое состояние после перезапуска программы.
2. Система должна уметь контролировать количество хранимых `Restore Point` (реализация алгоритма очистки точек)
3. `Merge` точек
4. Логирование
5. Восстановление