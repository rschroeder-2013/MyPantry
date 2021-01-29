-- Check if DB exists, drop if exists
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'my_pantry_db')
BEGIN
	DROP DATABASE my_pantry_db
	print '' print '*** dropping database my_pantry_db ***'
END
GO

print '' print '*** creating database my_pantry_db ***'
GO

CREATE DATABASE my_pantry_db
GO

print '' print '*** using my_pantry_db ***'
GO

USE my_pantry_db
GO

---------------------------------------------------------------------------------------------
-- Table Creation
---------------------------------------------------------------------------------------------

print '' print '*** creating mpuser table ***'
GO

CREATE TABLE [dbo].[MPUser](

	[MPUserID]		[int]				IDENTITY(100, 1)	NOT NULL,
	[Email]			[nvarchar](100)		NOT NULL,
	[FirstName]		[nvarchar](50)		NOT NULL,
	[LastName]		[nvarchar](100)		NOT NULL,
	[PhoneNumber]	[nvarchar](15)		NOT NULL,
	[PasswordHash]	[nvarchar](100)		NOT NULL DEFAULT
	'5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8',
	[Active]		[bit]				NOT NULL DEFAULT 1,
	CONSTRAINT	[pk_mpUserId] PRIMARY KEY ([MPUserID] ASC),
	CONSTRAINT	[ak_email] UNIQUE([Email] ASC)
	
)
GO

print '' print '*** creating MPUser test records ***'
GO

INSERT INTO [dbo].[MPUser]
	([Email], [FirstName], [LastName], [PhoneNumber])
	VALUES
	('rschroeder@mypantry.com', 'Rick', 'Schroeder', '5202701968'),
	('snobriga@mypantry.com', 'Sthefany', 'Nobriga', '3195944492'),
	('mstreet@mypantry.com', 'Monica', 'Street', '3198009927')
GO
	
---------------------------------------------------------------------------------------------

print '' print '*** creating role table ***'
GO

CREATE TABLE [dbo].[Role](

	[RoleID]		[nvarchar](25)		NOT NULL,
	[Description]	[nvarchar](250)		NULL,
	CONSTRAINT [pk_roleId] PRIMARY KEY([RoleID] ASC)

)
GO

print '' print '*** creating role test records ***'	
GO

INSERT INTO [dbo].[Role]
	([RoleID], [Description])
	VALUES
	('Admin', 'User administration'),
	('Premium User', 'Unlimited Recipies'),
	('Standard User', 'Limited Recipies')
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating mpuserrole table ***'	
GO

CREATE TABLE [dbo].[MPUserRole](

	[MPUserID]		[int]				NOT NULL,
	[RoleID]		[nvarchar](25)		NOT NULL,
	CONSTRAINT [pk_mpUserId_roleId] PRIMARY KEY([MPUserID], [RoldID]),
	CONSTRAINT [fk_mpUserId] FOREIGN KEY ([MPUserID]) REFERENCES [dbo].[MPUser]([MPUserID])

)
GO

print '' print '*** adding roleID foreign key to mpuser role table ***'	
GO

ALTER TABLE [dbo].[MPUserRole] WITH NOCHECK
	ADD CONSTRAINT [fk_roleId] FOREIGN KEY([RoleID])
		REFERENCES [dbo].[Role]([RoleID])
		ON UPDATE CASCADE
GO

print '' print '*** creating employee role test records ***'
GO

INSERT INTO [dbo].[MPUserRole]
([MPUserID], [RoleID])
VALUES
(100, 'Admin'),
(101, 'Premium User'),
(102, 'Standard User')
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating Ingredient table ***'
GO

CREATE TABLE [dbo].[Ingredient](

	[IngredientID]				[int]				IDENTITY(10000, 1)	NOT NULL,
	[IngredientName]			[nvarchar](50)		NOT NULL,
	[IngredientType]			[nvarchar](50)		NOT NULL,
	[IngredientDescription]		[nvarchar](255)		NULL,
	[MeasurementType]			[nvarchar](255)		NOT NULL,
	CONSTRAINT [pk_ingredientId] PRIMARY KEY([IngredientID] ASC)
)
GO

print '' print '*** creating ingredient test records ***'
GO

INSERT INTO [dbo].[Ingredient]
	([IngredientName], [IngredientType], [IngredientDescription], [MeasurementType])
	VALUES
	('Elbow Macaroni', 'Pasta', 'Barilla Elbow Macaroni', 'oz'),
	('Frozen Mixed Vegetables', 'Vegetables', 'Steamfresh Mixed Vegetables', 'bag'),
	('Canned Tuna', 'Meat', 'StarKist Canned Tuna', 'can'),
	('Cream of Mushroom Soup', 'Soup', 'Campbells Cream of Mushroom Soup', 'can'),
	('2% Milk', 'Dairy', 'Dairy Pure 2% Milk', 'cup'),
	('Mild Cheddar Cheese', 'Dairy', 'HyVee Mild Cheddar Shredded Cheese', 'oz'),
	('Lays Chips', 'Snack Food', 'Lays Potato Chips', 'bag')
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating Recipies table ***'
GO

CREATE TABLE [dbo].[Recipe](

	[MPUserID]				[int]				NOT NULL,				
	[RecipeID]				[int]				NOT NULL,
	[RecipeName]			[nvarchar](255)		NOT NULL,
	[IngredientID]			[int]				NOT NULL,
	[Quantity]				[decimal]			NOT NULL,
	CONSTRAINT [pk_recipeId] PRIMARY KEY([RecipeID] ASC),
	CONSTRAINT [fk_ingredientId] FOREIGN KEY ([IngredientID]) REFERENCES [dbo].[Ingredient]([IngredientID]),
	CONSTRAINT [fk_mpUserId] FOREIGN KEY ([MPUserID]) REFERENCES [dbo].[MPUser]([MPUserID])
	
)
GO

print '' print '*** creating recipes test records ***'
GO

INSERT INTO [dbo].[Recipe]
	([RecipeName], [IngredientID, [Quantity])
	VALUES
	('Tuna Casserole', 1, 16),
	('Tuna Casserole', 2, 1),
	('Tuna Casserole', 3, 4),
	('Tuna Casserole', 4, 1),
	('Tuna Casserole', 5, 1),
	('Tuna Casserole', 6, 16),
	('Tuna Casserole', 7, 2)
GO

---------------------------------------------------------------------------------------------
-- Stored Procedures for Users
---------------------------------------------------------------------------------------------

print '' print '*** STORED PROCEDURES FOR MY PANTRY USERS ***'	
GO

print '' print '*** creating sp_authenticate_mpuser ***'	
GO

CREATE PROCEDURE [dbo].[sp_authenticate_mpuser](

	@Email			[nvarchar](100),
	@PasswordHash	[nvarchar](100)

)
AS
	BEGIN
		SELECT COUNT(Email)
		FROM MPUser
		WHERE Email = @Email
			AND PasswordHash = @PasswordHash
			AND Active = 1
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_update_passwordHash ***'	
GO

CREATE PROCEDURE [dbo].[sp_update_passwordHash](

		@Email				[nvarchar](100),
		@OldPasswordHash	[nvarchar](100),
		@NewPasswordHash	[nvarchar](100)
		
	)
AS
	BEGIN
		UPDATE MPUser
			SET PasswordHash = @NewPasswordHash
			WHERE Email = @Email
				AND PasswordHash = @OldPasswordHash
		RETURN @@ROWCOUNT
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_select_mpuser_by_email ***'	
GO

CREATE PROCEDURE [dbo].[sp_select_mpuser_by_email](

		@Email				[nvarchar](100)
		
	)
AS
	BEGIN
		SELECT MPUserID, Email, FirstName, LastName, PhoneNumber, Active
		FROM MPUser
		WHERE Email = @Email	
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_select_mpuser_roles_by_mpuser_id ***'	
GO

CREATE PROCEDURE [dbo].[sp_select_mpuser_roles_by_mpuser_id](

		@MPUserID			[int]	
		
	)
AS
	BEGIN
		SELECT RoleID
		FROM MPUserRole
		WHERE MPUserID = @MPUserID	
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_update_mpuser_profile_data ***'	
GO

CREATE PROCEDURE [dbo].[sp_update_mpuser_profile_data](

		@MPUserID			[int],
		@OldEmail			[nvarchar](100),
		@OldFirstName		[nvarchar](50),
	    @OldLastName		[nvarchar](100),
		@OldPhoneNumber		[nvarchar](15),
		@NewEmail			[nvarchar](100),
		@NewFirstName		[nvarchar](50),
	    @NewLastName		[nvarchar](100),
		@NewPhoneNumber		[nvarchar](15)
		
	)
AS
	BEGIN
		UPDATE MPUser
			SET Email = @NewEmail,
				FirstName = @NewFirstName,
				LastName = @NewLastName,
				PhoneNumber = @NewPhoneNumber
			WHERE MPUserID = @MPUserID
			AND Email = @OldEmail
			AND	FirstName = @OldFirstName
			AND	LastName = @OldLastName
			AND	PhoneNumber = @OldPhoneNumber
		RETURN @@ROWCOUNT
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_select_ingredients_by_recipe_name ***'	
GO

CREATE PROCEDURE [dbo].[sp_select_ingredients_by_recipe_name](

		@RecipeName			[nvarchar](255)
		
	)
AS
	BEGIN
		SELECT IngredientName
		FROM Recipe JOIN Ingredient
		ON Recipe.IngredientID = Ingredient.IngredientID
		WHERE RecipeName = @RecipeName
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_select_all_recipes ***'	
GO

CREATE PROCEDURE [dbo].[sp_select_all_recipes]
AS
	BEGIN
		SELECT RecipeID, RecipeName
		FROM Recipes
		ORDER BY RecipeName ASC
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_select_all_ingredients ***'	
GO

CREATE PROCEDURE [dbo].[sp_select_all_ingredients]
AS
	BEGIN
		SELECT IngredientID, IngredientName, IngredientType
		FROM Ingredient
		ORDER BY RecipeName ASC
	END
GO

---------------------------------------------------------------------------------------------
-- Stored Procedures for Administrators
---------------------------------------------------------------------------------------------

print '' print '*** EMPLOYEE STORED PROCEDURES FOR ADMINS ***'	
GO

print '' print '*** creating sp_insert_new_mpuser ***'	
GO

CREATE PROCEDURE [dbo].[sp_insert_new_mpuser](

		@Email				[nvarchar](100),
		@FirstName			[nvarchar](50),
	    @LastName			[nvarchar](100),
		@PhoneNumber		[nvarchar](15)
		
	)
AS
	BEGIN
		INSERT INTO MPUser
			([Email], [FirstName], [LastName], [PhoneNumber])
		VALUES
			(@Email, @FirstName, @LastName, @PhoneNumber)
		SELECT SCOPE_IDENTITY()	
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_select_all_mpusers ***'	
GO

CREATE PROCEDURE [dbo].[sp_select_all_mpusers]
AS
	BEGIN
		SELECT MPUserID, Email, FirstName, LastName, PhoneNumber
		FROM MPUser
		ORDER BY LastName ASC
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_select_all_mpusers_by_active ***'	
GO

CREATE PROCEDURE [dbo].[sp_select_all_mpusers_by_active](

		@Active				[bit]
		
	)
AS
	BEGIN
		SELECT MPUserID, Email, FirstName, LastName, PhoneNumber, Active
		FROM MPUser
		WHERE Active = @Active
		ORDER BY LastName ASC
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_select_all_mpusers_by_id ***'	
GO

CREATE PROCEDURE [dbo].[sp_select_all_employee_by_id](

		@EmployeeID			[int]
		
	)
AS
	BEGIN
		SELECT EmployeeID, Email, FirstName, LastName, PhoneNumber
		FROM Employee
		WHERE EmployeeID = @EmployeeID
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_reset_passwordHash ***'	
GO

CREATE PROCEDURE [dbo].[sp_reset_passwordHash](

		@Email				[nvarchar](100)
		
	)
AS
	BEGIN
		UPDATE MPUser
			SET PasswordHash = 
			'5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8'
			WHERE Email = @Email
		RETURN @@ROWCOUNT
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_deactivate_mpuser ***'	
GO

CREATE PROCEDURE [dbo].[sp_deactivate_mpuser](

		@MPUserID			[int]
		
	)
AS
	BEGIN
		UPDATE MPUser
			SET Active = 0
			WHERE MPUserID = @MPUserID
		RETURN @@ROWCOUNT	
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_reactivate_mpuser ***'	
GO

CREATE PROCEDURE [dbo].[sp_reactivate_mpuser](

		@MPUserID			[int]
		
	)
AS
	BEGIN
		UPDATE MPUser
			SET Active = 1
			WHERE MPUserID = @MPUserID
		RETURN @@ROWCOUNT	
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_add_mpuser_role***'	
GO

CREATE PROCEDURE [dbo].[sp_add_mpuser_role](

		@MPUserID			[int],
		@RoleID				[nvarchar](25)
		
	)
AS
	BEGIN
		INSERT INTO MPUserRole
			([MPUserID], [RoleID])
		VALUES
			(@MPUserID, @RoleID)
		RETURN @@ROWCOUNT
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_remove_mpuser_role***'	
GO

CREATE PROCEDURE [dbo].[sp_remove_mpuser_role](

		@EmployeeID			[int],
		@RoleID				[nvarchar](25)
		
	)
AS
	BEGIN
		DELETE FROM MPUserRole
			WHERE MPUserID = @MPUserID
			AND RoleID = @RoleID
		RETURN @@ROWCOUNT
	END
GO









































