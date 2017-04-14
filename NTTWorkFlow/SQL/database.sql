﻿USE [master]

/****** Object:  Database [MyWorkFlow]    Script Date: 14/04/2017 12:17:14 ******/
CREATE DATABASE [MyWorkFlow]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MyWorkFlow', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\MyWorkFlow.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MyWorkFlow_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\MyWorkFlow_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)

ALTER DATABASE [MyWorkFlow] SET COMPATIBILITY_LEVEL = 120

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MyWorkFlow].[dbo].[sp_fulltext_database] @action = 'enable'
end

ALTER DATABASE [MyWorkFlow] SET ANSI_NULL_DEFAULT OFF 

ALTER DATABASE [MyWorkFlow] SET ANSI_NULLS OFF 

ALTER DATABASE [MyWorkFlow] SET ANSI_PADDING OFF 

ALTER DATABASE [MyWorkFlow] SET ANSI_WARNINGS OFF 

ALTER DATABASE [MyWorkFlow] SET ARITHABORT OFF 

ALTER DATABASE [MyWorkFlow] SET AUTO_CLOSE OFF 

ALTER DATABASE [MyWorkFlow] SET AUTO_SHRINK OFF 

ALTER DATABASE [MyWorkFlow] SET AUTO_UPDATE_STATISTICS ON 

ALTER DATABASE [MyWorkFlow] SET CURSOR_CLOSE_ON_COMMIT OFF 

ALTER DATABASE [MyWorkFlow] SET CURSOR_DEFAULT  GLOBAL 

ALTER DATABASE [MyWorkFlow] SET CONCAT_NULL_YIELDS_NULL OFF 

ALTER DATABASE [MyWorkFlow] SET NUMERIC_ROUNDABORT OFF 

ALTER DATABASE [MyWorkFlow] SET QUOTED_IDENTIFIER OFF 

ALTER DATABASE [MyWorkFlow] SET RECURSIVE_TRIGGERS OFF 

ALTER DATABASE [MyWorkFlow] SET  DISABLE_BROKER 

ALTER DATABASE [MyWorkFlow] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 

ALTER DATABASE [MyWorkFlow] SET DATE_CORRELATION_OPTIMIZATION OFF 

ALTER DATABASE [MyWorkFlow] SET TRUSTWORTHY OFF 

ALTER DATABASE [MyWorkFlow] SET ALLOW_SNAPSHOT_ISOLATION OFF 

ALTER DATABASE [MyWorkFlow] SET PARAMETERIZATION SIMPLE 

ALTER DATABASE [MyWorkFlow] SET READ_COMMITTED_SNAPSHOT OFF 

ALTER DATABASE [MyWorkFlow] SET HONOR_BROKER_PRIORITY OFF 

ALTER DATABASE [MyWorkFlow] SET RECOVERY FULL 

ALTER DATABASE [MyWorkFlow] SET  MULTI_USER 

ALTER DATABASE [MyWorkFlow] SET PAGE_VERIFY CHECKSUM  

ALTER DATABASE [MyWorkFlow] SET DB_CHAINING OFF 

ALTER DATABASE [MyWorkFlow] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 

ALTER DATABASE [MyWorkFlow] SET TARGET_RECOVERY_TIME = 0 SECONDS 

ALTER DATABASE [MyWorkFlow] SET DELAYED_DURABILITY = DISABLED 

ALTER DATABASE [MyWorkFlow] SET  READ_WRITE 

EXEC sys.sp_db_vardecimal_storage_format N'MyWorkFlow', N'ON'