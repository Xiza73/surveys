CREATE TABLE Category (
  id_category INT IDENTITY(1, 1) PRIMARY KEY,
  name VARCHAR(50) NOT NULL
);

CREATE TABLE Question (
  id_question INT IDENTITY(1, 1) PRIMARY KEY,
  id_category INT NOT NULL,
  content VARCHAR(200) NOT NULL,
  type INT NOT NULL,
  active BIT DEFAULT 1,
  FOREIGN KEY (id_category) REFERENCES category(id_category)
);

CREATE TABLE Branch (
  id_branch INT IDENTITY(1, 1) PRIMARY KEY,
  name VARCHAR(50) NOT NULL,
  city VARCHAR(50) NOT NULL,
  province VARCHAR(50) NOT NULL,
  active BIT DEFAULT 1
);

CREATE TABLE Person (
  id_person INT IDENTITY(1, 1) PRIMARY KEY,
  identification VARCHAR(50) NOT NULL,
  name VARCHAR(50) NOT NULL,
  sex VARCHAR(50) NOT NULL,
  age INT NOT NULL,
  active BIT DEFAULT 1
);

CREATE TABLE Survey (
  id_survey INT IDENTITY(1, 1) PRIMARY KEY,
  id_branch INT NOT NULL,
  id_person INT NOT NULL,
  date DATE DEFAULT GETDATE(),
  active BIT DEFAULT 1,
  percentage INT NOT NULL DEFAULT 0,
  FOREIGN KEY (id_branch) REFERENCES branch(id_branch),
  FOREIGN KEY (id_person) REFERENCES person(id_person)
);

CREATE TABLE SurveyQuestion (
  id_survey INT NOT NULL,
  id_question INT NOT NULL,
  answer VARCHAR(200) NOT NULL,
  percentage INT NOT NULL,
  PRIMARY KEY (id_survey, id_question),
  FOREIGN KEY (id_survey) REFERENCES survey(id_survey),
  FOREIGN KEY (id_question) REFERENCES question(id_question)
);

/* INSERTING DATA */
-- categories and questions
DECLARE @category_id INT;

INSERT INTO
  Category (name)
VALUES
  ('Estándares');

SET
  @category_id = SCOPE_IDENTITY();

INSERT INTO
  Question (id_category, content, type)
VALUES
  (
    @category_id,
    'Le despacharon los productos revisando la factura',
    1
  ),
  (@category_id, 'Le entregaron la factura', 1);

INSERT INTO
  Category (name)
VALUES
  ('Satisfacción');

SET
  @category_id = SCOPE_IDENTITY();

INSERT INTO
  Question (id_category, content, type)
VALUES
  (@category_id, 'La amabilidad del cajero', 2),
  (@category_id, 'La satisfacción en general', 2);

INSERT INTO
  Category (name)
VALUES
  ('Comentarios');

SET
  @category_id = SCOPE_IDENTITY();

INSERT INTO
  Question (id_category, content, type)
VALUES
  (
    @category_id,
    'En qué nivel recomendaría XYZ a amigos o familiares siendo 0: definitivamente no lo recomendaría y 10: sí lo recomendaría',
    2
  ),
  (
    @category_id,
    'Sus observaciones finales por favor',
    3
  );

-- branches
INSERT INTO
  Branch (name, city, province)
VALUES
  ('29 de Mayo', 'Loja', 'Loja'),
  ('Tarqui', 'Pedernales', 'Manabí'),
  ('San Rafael', 'Rumiñahui', 'Pichincha'),
  ('Guasmo', 'Guayaquil', 'Guayas'),
  ('Mena 2', 'Quito', 'Pichincha'),
  ('Quicentro Norte', 'Quito', 'Pichincha');

/* STORED PROCEDURES */
USE db_retail
/* Get Questions */
GO
  IF OBJECT_ID(' dbo.sp_get_questions ', ' P ') IS NOT NULL DROP PROCEDURE dbo.sp_get_questions
GO
  CREATE PROCEDURE dbo.sp_get_questions AS BEGIN
SELECT
  q.id_question AS id,
  q.content AS question,
  q.type AS type,
  c.name AS category
FROM
  question q
  INNER JOIN category c ON c.id_category = q.id_category
WHERE
  q.active = 1
END
/* Get Branches */
GO
  IF OBJECT_ID(' dbo.sp_get_branches ', ' P ') IS NOT NULL DROP PROCEDURE dbo.sp_get_branches
GO
  CREATE PROCEDURE dbo.sp_get_branches AS BEGIN
SELECT
  id_branch,
  name,
  city,
  province
FROM
  branch
WHERE
  active = 1
END
GO
  EXEC sp_get_branches
  /* Save Survey */
GO
  IF OBJECT_ID(' dbo.sp_save_survey ', ' P ') IS NOT NULL DROP PROCEDURE dbo.sp_save_survey
GO
  CREATE PROCEDURE dbo.sp_save_survey @id_branch INT,
  @id_person INT,
  @questions NVARCHAR(MAX) AS BEGIN DECLARE @id_survey INT,
  @id_question INT,
  @answer VARCHAR(200),
  @percentage INT
INSERT INTO
  survey (id_branch, id_person)
VALUES
  (@id_branch, @id_person);

SET
  @id_survey = SCOPE_IDENTITY();

SELECT
  @id_survey AS id_survey;

DECLARE @survey_question TABLE (
  id_survey INT,
  id_question INT,
  answer VARCHAR(200),
  percentage INT
);

INSERT INTO
  @survey_question (id_survey, id_question, answer, percentage)
SELECT
  @id_survey,
  id,
  value,
  percentage
FROM
  OPENJSON(@questions) WITH (
    id INT '$.id',
    value VARCHAR(200) '$.value',
    percentage INT '$.percentage'
  );

INSERT INTO
  surveyquestion (id_survey, id_question, answer, percentage)
SELECT
  id_survey,
  id_question,
  answer,
  percentage
FROM
  @survey_question;

SELECT
  @percentage = AVG(percentage)
FROM
  @survey_question;

UPDATE
  survey
SET
  percentage = @percentage
WHERE
  id_survey = @id_survey;

SELECT
  @id_survey AS id_survey;

END
/* Get Survey */
GO
  IF OBJECT_ID(' dbo.sp_get_survey ', ' P ') IS NOT NULL DROP PROCEDURE dbo.sp_get_survey
GO
  CREATE PROCEDURE dbo.sp_get_survey @id_survey INT AS BEGIN
SELECT
  s.id_survey,
  s.id_branch,
  s.id_person,
  s.date,
  s.percentage,
  sq.id_question,
  q.content AS question,
  sq.answer
FROM
  survey s
  INNER JOIN surveyquestion sq ON sq.id_survey = s.id_survey
  INNER JOIN question q ON q.id_question = sq.id_question
WHERE
  s.id_survey = @id_survey
END
/* Get Surveys */
GO
  IF OBJECT_ID(' dbo.sp_get_surveys ', ' P ') IS NOT NULL DROP PROCEDURE dbo.sp_get_surveys
GO
  CREATE PROCEDURE dbo.sp_get_surveys AS BEGIN
SELECT
  s.id_survey,
  s.date,
  b.name AS branch,
  p.name AS person
FROM
  survey s
  INNER JOIN branch b ON b.id_branch = s.id_branch
  INNER JOIN person p ON p.id_person = s.id_person
WHERE
  s.active = 1
END
/* Get Surveys by Branch */
GO
  IF OBJECT_ID(' dbo.sp_get_surveys_by_branch ', ' P ') IS NOT NULL DROP PROCEDURE dbo.sp_get_surveys_by_branch
GO
  CREATE PROCEDURE dbo.sp_get_surveys_by_branch @id_branch INT AS BEGIN
SELECT
  s.id_survey,
  s.date,
  s.percentage,
  b.name AS branch,
  p.name AS person
FROM
  survey s
  INNER JOIN branch b ON b.id_branch = s.id_branch
  INNER JOIN person p ON p.id_person = s.id_person
WHERE
  s.active = 1
  AND s.id_branch = @id_branch
END
/* Create Person */
GO
  IF OBJECT_ID(' dbo.sp_create_person ', ' P ') IS NOT NULL DROP PROCEDURE dbo.sp_create_person
GO
  CREATE PROCEDURE dbo.sp_create_person @name VARCHAR(50),
  @identification VARCHAR(50),
  @sex VARCHAR(50),
  @age INT AS BEGIN
INSERT INTO
  Person (name, identification, sex, age)
VALUES
  (@name, @identification, @sex, @age)
END
/* Get Persons */
GO
  IF OBJECT_ID(' dbo.sp_get_persons ', ' P ') IS NOT NULL DROP PROCEDURE dbo.sp_get_persons
GO
  CREATE PROCEDURE dbo.sp_get_persons AS BEGIN
SELECT
  id_person,
  name,
  identification
FROM
  person
WHERE
  active = 1
END
/* Get Person */
GO
  IF OBJECT_ID(' dbo.sp_get_person ', ' P ') IS NOT NULL DROP PROCEDURE dbo.sp_get_person
GO
  CREATE PROCEDURE dbo.sp_get_person @id_person INT AS BEGIN
SELECT
  id_person,
  name,
  identification,
  sex,
  age
FROM
  person
WHERE
  active = 1
  AND id_person = @id_person
END