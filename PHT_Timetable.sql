-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Хост: 127.0.0.1
-- Время создания: Апр 29 2019 г., 18:37
-- Версия сервера: 10.1.10-MariaDB
-- Версия PHP: 5.6.19

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `schedulekpd`
--

-- --------------------------------------------------------

--
-- Структура таблицы `group_rp`
--

CREATE TABLE `group_rp` (
  `GroupNumber` int(11) NOT NULL,
  `Monday` text CHARACTER SET utf8 NOT NULL,
  `Tuesday` text CHARACTER SET utf8 NOT NULL,
  `Wednesday` text CHARACTER SET utf8 NOT NULL,
  `Thursday` text CHARACTER SET utf8 NOT NULL,
  `Friday` text CHARACTER SET utf8 NOT NULL,
  `Saturday` text CHARACTER SET utf8 NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Дамп данных таблицы `group_rp`
--

INSERT INTO `group_rp` (`GroupNumber`, `Monday`, `Tuesday`, `Wednesday`, `Thursday`, `Friday`, `Saturday`) VALUES
(625, '0/ПАХТ/2/0/321/Лызлова М.В.!1/ТОХТПЭиУМ/3/0/321/Логинов В.С.!2/ТТиТ/3/0/405/Мельник Г.И.!1/ЭПвХТ/4/0/321/Воробьева Е.В.!2/ТТиТ/4/1/328/Мельник Г.И.', '0/Военная кафедра/0/4//', '1/ОАТП/2/0/328/Коваленко В.В.!2/ОАТП/2/1/328/Коваленко В.В.-0/Физ-ра/3!3/ОАТП/4/3/328/Коваленко В.В./13.02,13.03,10.04,08.05', '2/ТТиТ/2/3/328/Мельник Г.И./21.02.,21.03.,18.04.,16.05.!1/ЭПвХТ/3/1/328/Лызлова М.В.!0/ХТПЭиУМ/4/0/321/Лызлова М.В.!0/ХТПЭиУМ/5/1/328/Лызлова М.В.', '1/ПАХТ/2/3/328/Лызлова М.В.!2/ХТПЭиУМ/2/3/409/Шуварикова Т.П./08.03.,05.04.,03.05.,31.05.!0/ТОХТПЭиУМ/4/0/321/Логинов В.С.!0/ТОХТПЭиУМ/5/1/315/Логинов В.С.', ''),
(6215, '0/ПАХТ/2/0/321/Лызлова М. В.!1/ПАХТ/3/3/328/Лызлова М. В.!2/ТТиТ/3/0/405/Мельник Г.И.!2/ТТиТ/4/1/328/Мельник Г.И.!1/Эк.бкз/4/0/321/Воробьева Е.В.!2/ТОЭ/4/1/321/Воробьева Е.В.', '0/Военная кафедра/0/4//', '1/ОАТП/2/0/328/Коваленко В.В.!2/ОАТП/2/1/328/Коваленко В.В.!0/Физкультура/3/2//!2/ОАТП/4/3/328/Коваленко В.В./27.02,27.03,24.04,22.05', '1/ЭХТ/1/0/326/Качанова Л.П.!1/ЭХТ/2/1/326/Качанова Л.П.!2/ЭХТ/2/3/326/Качанова Л.П./21.02.,21.03.,18.04.16.05.!2/ТТиТ/2/3/328/Мельник Г.И./07.03.,04.04.,02.05.,30.05.!2/ЭХТ/3/0/328/Качанова Л.П.!2/ЭХТ/4/1/321/Качанова Л.П.', '1/Эк.без./2/1/321/Воробьева Е.В.!1/ТОЭ/3/0/321/Воробьева Е.В.!1/ТОЭ/4/1/321/Воробьева Е.В.!2/ТОЭ/4/0/321/Воробьева Е.В.!2/ТОЭ/5/1/321/Воробьева Е.В.', '');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `group_rp`
--
ALTER TABLE `group_rp`
  ADD UNIQUE KEY `GroupNumber` (`GroupNumber`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
