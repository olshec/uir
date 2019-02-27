-- phpMyAdmin SQL Dump
-- version 3.5.1
-- http://www.phpmyadmin.net
--
-- Хост: 127.0.0.1
-- Время создания: Фев 06 2018 г., 10:11
-- Версия сервера: 5.5.25
-- Версия PHP: 5.3.13

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- База данных: `mydb1`
--

-- --------------------------------------------------------

--
-- Структура таблицы `plan`
--

CREATE TABLE IF NOT EXISTS `plan` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `trass` varchar(45) NOT NULL,
  `plane_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_plan_plane1_idx` (`plane_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=12 ;

--
-- Дамп данных таблицы `plan`
--

INSERT INTO `plan` (`id`, `name`, `trass`, `plane_id`) VALUES
(3, 'План 1', 'Трасса 1', 9),
(7, 'План 2', 'Трасса 2', 9),
(8, 'План 3', 'Трасса 3', 10),
(9, 'План 4', 'Трасса 2', 10),
(10, 'План 5', 'Трасса 3', 10),
(11, 'План 6', 'Трасса 2', 9);

-- --------------------------------------------------------

--
-- Структура таблицы `plane`
--

CREATE TABLE IF NOT EXISTS `plane` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `number` varchar(45) NOT NULL,
  `speed` float NOT NULL,
  `takeoff_weight` float NOT NULL,
  `mass_fuel` float NOT NULL,
  `cost_fuel` float NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `number_UNIQUE` (`number`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=11 ;

--
-- Дамп данных таблицы `plane`
--

INSERT INTO `plane` (`id`, `name`, `number`, `speed`, `takeoff_weight`, `mass_fuel`, `cost_fuel`) VALUES
(9, 'Boeing 777-300', '15', 905, 66050, 171170, 2139.62),
(10, 'Airbus 310', '250', 895, 33550, 54920, 1194);

-- --------------------------------------------------------

--
-- Структура таблицы `plan_ppm`
--

CREATE TABLE IF NOT EXISTS `plan_ppm` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `ppm_id` int(11) NOT NULL,
  `plan_id` int(11) NOT NULL,
  `number_ppm` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_plan_ppm_ppm1_idx` (`ppm_id`),
  KEY `fk_plan_ppm_plan1_idx` (`plan_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=23 ;

--
-- Дамп данных таблицы `plan_ppm`
--

INSERT INTO `plan_ppm` (`id`, `ppm_id`, `plan_id`, `number_ppm`) VALUES
(7, 9, 3, 1),
(8, 10, 3, 2),
(9, 11, 7, 1),
(10, 16, 7, 2),
(11, 17, 7, 3),
(12, 15, 8, 1),
(13, 17, 8, 2),
(14, 12, 9, 1),
(15, 10, 9, 2),
(16, 12, 9, 1),
(17, 12, 3, 1),
(18, 15, 3, 2),
(19, 17, 3, 3),
(20, 12, 11, 1),
(21, 15, 11, 2),
(22, 11, 11, 3);

-- --------------------------------------------------------

--
-- Структура таблицы `ppm`
--

CREATE TABLE IF NOT EXISTS `ppm` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `latitude` float NOT NULL,
  `longitude` float NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=18 ;

--
-- Дамп данных таблицы `ppm`
--

INSERT INTO `ppm` (`id`, `name`, `latitude`, `longitude`) VALUES
(9, 'Москва', 55.75, 37.5),
(10, 'Нью-Йорк', 40.66, -74.03),
(11, 'Анапа', 44.89, 37.33),
(12, 'Архангельск', 84.5, 40.5),
(15, 'Екатеринбург', 46.8, 60.7),
(16, 'Атланта', 33.75, -84.4),
(17, 'Чикаго', 41.9, -87.6);

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `plan`
--
ALTER TABLE `plan`
  ADD CONSTRAINT `fk_plan_plane1` FOREIGN KEY (`plane_id`) REFERENCES `plane` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `plan_ppm`
--
ALTER TABLE `plan_ppm`
  ADD CONSTRAINT `fk_plan_ppm_plan1` FOREIGN KEY (`plan_id`) REFERENCES `plan` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_plan_ppm_ppm1` FOREIGN KEY (`ppm_id`) REFERENCES `ppm` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
