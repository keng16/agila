-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Oct 05, 2016 at 08:03 PM
-- Server version: 10.1.10-MariaDB
-- PHP Version: 7.0.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `agiladb`
--
CREATE DATABASE IF NOT EXISTS `agiladb` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `agiladb`;

-- --------------------------------------------------------

--
-- Table structure for table `finished_product`
--

CREATE TABLE `finished_product` (
  `fin_id` int(6) NOT NULL,
  `fin_name` text NOT NULL,
  `fin_desc` text NOT NULL,
  `Quantity` int(11) NOT NULL,
  `Price` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `finished_product`
--

INSERT INTO `finished_product` (`fin_id`, `fin_name`, `fin_desc`, `Quantity`, `Price`) VALUES
(100000, 'ken', 'ken', 100, 100),
(100001, 'aslskd', 'as;ldj', 100, 100),
(100002, 'kkk', 'kkk', 102, 5000),
(100003, 'kkkasdk', 'alskdj', 100, 100),
(100004, 'Table', 'kenonne john', 100, 100);

-- --------------------------------------------------------

--
-- Table structure for table `log`
--

CREATE TABLE `log` (
  `userName` text NOT NULL,
  `LogInDate` text NOT NULL,
  `LogInTime` text NOT NULL,
  `LogOutDate` text NOT NULL,
  `LogOutTime` text NOT NULL,
  `userTitle` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Log Book of Users';

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `userName` text NOT NULL,
  `userPass` longtext NOT NULL,
  `userFirstname` text NOT NULL,
  `userLastname` text NOT NULL,
  `userContactnum` varchar(11) NOT NULL,
  `userBirthdate` text NOT NULL,
  `userAddress` text NOT NULL,
  `userTitle` text NOT NULL,
  `userPicture` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`userName`, `userPass`, `userFirstname`, `userLastname`, `userContactnum`, `userBirthdate`, `userAddress`, `userTitle`, `userPicture`) VALUES
('Admin', '123', 'x', 'x', '1', '1/1/2016', 'x', '', ''),
('fahad', '123', 'x', 'x', '1', '1/1/2016', 'x', '', ''),
('ken', 'ken', 'kenonnejon', 'ken', '0123', '1/1/2016', 'ken', '', '');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `finished_product`
--
ALTER TABLE `finished_product`
  ADD PRIMARY KEY (`fin_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`userName`(255));

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
