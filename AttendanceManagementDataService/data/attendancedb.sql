-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 28, 2026 at 06:45 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `attendancedb`
--

-- --------------------------------------------------------

--
-- Table structure for table `attendancerecord`
--

CREATE TABLE `attendancerecord` (
  `Id` int(11) NOT NULL,
  `StudentName` varchar(100) DEFAULT NULL,
  `DayIndex` int(11) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `attendancerecord`
--

INSERT INTO `attendancerecord` (`Id`, `StudentName`, `DayIndex`, `Status`) VALUES
(1, 'karlo', 0, 0),
(2, 'neil', 0, 0),
(3, 'roderick', 0, 0),
(4, 'karlo', 1, 0),
(5, 'neil', 1, 1),
(6, 'roderick', 1, 1),
(7, 'karlo', 2, 0),
(8, 'neil', 2, 0),
(9, 'roderick', 2, 0),
(38, 'mark', 0, 0),
(42, 'mark', 1, 1),
(46, 'mark', 2, 0),
(50, 'kurt', 0, 0),
(55, 'kurt', 1, 1),
(60, 'kurt', 2, 0);

-- --------------------------------------------------------

--
-- Table structure for table `students`
--

CREATE TABLE `students` (
  `StudentId` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `students`
--

INSERT INTO `students` (`StudentId`, `Name`) VALUES
(4, 'karlo'),
(10, 'kurt'),
(9, 'mark'),
(1, 'neil'),
(2, 'roderick');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `attendancerecord`
--
ALTER TABLE `attendancerecord`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `unique_attendance` (`StudentName`,`DayIndex`),
  ADD UNIQUE KEY `unique_att` (`StudentName`,`DayIndex`);

--
-- Indexes for table `students`
--
ALTER TABLE `students`
  ADD PRIMARY KEY (`StudentId`),
  ADD UNIQUE KEY `Name` (`Name`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `attendancerecord`
--
ALTER TABLE `attendancerecord`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=80;

--
-- AUTO_INCREMENT for table `students`
--
ALTER TABLE `students`
  MODIFY `StudentId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `attendancerecord`
--
ALTER TABLE `attendancerecord`
  ADD CONSTRAINT `fk_student_name` FOREIGN KEY (`StudentName`) REFERENCES `students` (`Name`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
