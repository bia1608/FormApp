# Full Stack Web Application (ASP.NET Core + React + SQL Server + Docker)

## Overview
This project is a full-stack web application that allows students to submit a form. The backend is built with **ASP.NET Core** and the frontend using **React**. The data is saved in a **SQL Server** database, and a **PDF** containing the submitted data is generated for download. The app is containerized using **Docker**.

## Technologies Used
- **Backend**: ASP.NET Core Web API (C#)
- **Frontend**: React
- **Database**: SQL Server
- **PDF Generation**: DinkToPdf
- **Containerization**: Docker + Docker Compose

## Features
### Frontend (Student Form):
- **Fields**:
  - Nume (input text)
  - Prenume (input text)
  - Facultate (input text)
  - Motivare (minimum 100 characters)

![image](https://github.com/user-attachments/assets/455f5c66-c484-459b-b2c9-44b64d62b8ca)


- **Actions**:
  - On submission, the form sends data to the backend, which:
    - Saves the data in the database
    - Generates a personalized PDF
    - Returns the PDF as a downloadable file.

- **PDF Generator**:
  - Includes title: "Fisa Studentului"
  - Data submitted by the student
  - Generation date
  - Signature field

![image](https://github.com/user-attachments/assets/f1a860f9-93ca-414e-ba04-0bad72b0177c)

## Compiling instructions
## 1. Clone project

```bash
git clone -o aspnet-starter-kit -b master --single-branch \ https://github.com/kriasoft/aspnet-starter-kit.git MyApp
cd MyApp
```
## 2. Install project dependencies
```bash
npm install
```
The app should be available at  http://localhost:808/

## 3. Launch app
```bash
npm start
```
