# OOP-Tetris
Игра тетрис написанная в объекто ориентированной парадигме. Сделано для лабораторной работы во время учебы в вузе на 2-3 курсе. 

## О Проекте
Игра была написана для курса ООП часть 2. Одной из задач было использование наследования, инкапсуляции и полиморфизма в программе.
Интерфейс был создан с помощью WinForms. В центре окна элемент PictureBox на который выводится состояние поля игры. 
Обновление хода игры и интерфейса осуществляется с помощью таймера. 

## Модель 
WinForms отвечают за взаимодействие с пользователем, вывод музыки и изображения. Логика игры хранится в классе Game. 
Публичный интерфейс класса Game позволяет управлять движением игры и течением времени. С помощью публичных полей можно узнать счет 
состояние поля игры и состояние текущей фигуры. Есть несколько событий, используемых для обновления UI.

Фигуры представлены в виде ииерархии обьектов. Базовый класс обьявляет абстрактные поля и методы, общие для каждой фигуры. 
Остальные методы наследуют от базового и переопределяют абстрактные методы. Схема классов (упрощенная)
  * FallingFigure
    * FallingFigureStandart
      * TFigure
      * Zfigure
    * Bonus

## Особенности
Счета игроков хранятся вместе в отдельном бинарном файле. Каждая отдельная запись - 7 байт
  * [4байта - Счет]
  * [1байт - день]
  * [1байт - месяц]
  * [1байт - год(начиная с 2000го)]

В верхнем правом углу экрана есть кнопка с нотой - она включает музыку. Музыка должна располагаться в папке Resourses/Musiс. 
Вместе с игрой идут 3 трека с audionautix.com, которые распространяются по лицензии СC 3.0;
Resourses/Music.

Игра может воспроизводить и другие треки с расширениями ".mp3", ".wav", ".m4a", ".m4v", ".mov", ".mp4" в папке Resourses/Music. 
За воспроизведение музыки отвечает библиотека NAudio.
