-- Check if DB exists, drop if exists
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'my_pantry_db2')
BEGIN
	DROP DATABASE my_pantry_db2
	print '' print '*** dropping database my_pantry_db2 ***'
END
GO

print '' print '*** creating database my_pantry_db2 ***'
GO

CREATE DATABASE my_pantry_db2
GO

print '' print '*** using my_pantry_db ***'
GO

USE my_pantry_db2
GO

---------------------------------------------------------------------------------------------
-- Table Creation
---------------------------------------------------------------------------------------------

print '' print '*** creating mpuser table ***'
GO

CREATE TABLE [dbo].[MPUser]
	(

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

CREATE TABLE [dbo].[Role]
	(

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

CREATE TABLE [dbo].[MPUserRole]
	(

		[MPUserID]		[int]				NOT NULL,
		[RoleID]		[nvarchar](25)		NOT NULL,
		CONSTRAINT [pk_mpUserId_roleId] PRIMARY KEY([MPUserID], [RoleID]),
		CONSTRAINT [fk_mpUserId_mpUserRoleTbl] FOREIGN KEY ([MPUserID]) REFERENCES [dbo].[MPUser]([MPUserID])

	)
GO

print '' print '*** adding roleID foreign key to mpuser role table ***'	
GO

ALTER TABLE [dbo].[MPUserRole] WITH NOCHECK 
	ADD CONSTRAINT [fk_roleID] FOREIGN KEY([RoleID]) 
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

CREATE TABLE [dbo].[Ingredient]
	(

		[IngredientID]				[int]				IDENTITY(100, 1)	NOT NULL,
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

CREATE TABLE [dbo].[Recipe]
	(
					
		[RecipeID]				[int]				IDENTITY(100, 1)		NOT NULL,
		[RecipeName]			[nvarchar](255)		NOT NULL,
		CONSTRAINT [pk_recipeId] PRIMARY KEY([RecipeID] ASC)
		
	)
GO

print '' print '*** creating recipes test records ***'
GO

INSERT INTO [dbo].[Recipe]
	([RecipeName])
	VALUES
	('Tuna Casserole')
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating RecipieIngredient table ***'
GO

CREATE TABLE [dbo].[RecipeIngredient]
	(
						
		[RecipeID]				[int]				NOT NULL,
		[IngredientID]			[int]				NOT NULL,
		[Quantity]				[decimal](4,2)		NOT NULL,
		CONSTRAINT [fk_ingredientID_recipeIngredientTbl] FOREIGN KEY ([IngredientID]) REFERENCES [dbo].[Ingredient]([IngredientID]) ON DELETE CASCADE
		
	)
GO

INSERT INTO [dbo].[RecipeIngredient]
	([RecipeID], [IngredientID], [Quantity])
	VALUES
	(100, 100, "16.00"),
	(100, 101, "1.00"),
	(100, 102, "4.00"),
	(100, 103, "1.00"),
	(100, 104, "1.00"),
	(100, 105, "16.00"),
	(100, 106, "1.00")
GO
---------------------------------------------------------------------------------------------
-- Stored Procedures for Users
---------------------------------------------------------------------------------------------

print '' print '*** STORED PROCEDURES FOR MY PANTRY USERS ***'	
GO

print '' print '*** creating sp_authenticate_mpuser ***'	
GO

CREATE PROCEDURE [dbo].[sp_authenticate_mpuser]
	(

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

print '' print '*** creating sp_update_ingredient_data ***'	
GO

CREATE PROCEDURE [dbo].[sp_update_ingredient_data]
	(
	
		@IngredientID					[int],
		@OldIngredientName				[nvarchar](50),
		@OldIngredientType				[nvarchar](50),
	    @OldIngredientDescription		[nvarchar](255),
		@OldMeasurementType				[nvarchar](255),
		@NewIngredientName				[nvarchar](50),
		@NewIngredientType				[nvarchar](50),
	    @NewIngredientDescription		[nvarchar](255),
		@NewMeasurementType				[nvarchar](255)
		
	)
AS
	BEGIN
		UPDATE Ingredient
			SET IngredientName = @NewIngredientName,
				IngredientType = @NewIngredientType,
				IngredientDescription = @NewIngredientDescription,
				MeasurementType = @NewMeasurementType
			WHERE IngredientID = @IngredientID
			AND IngredientName = @OldIngredientName
			AND	IngredientType = @OldIngredientType
			AND	IngredientDescription = @OldIngredientDescription
			AND	MeasurementType = @OldMeasurementType
		RETURN @@ROWCOUNT
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_insert_new_ingredient ***'	
GO

CREATE PROCEDURE [dbo].[sp_insert_new_ingredient]
	(

		@IngredientName				[nvarchar](50),
		@IngredientType				[nvarchar](50),
	    @IngredientDescription		[nvarchar](255),
		@MeasurementType			[nvarchar](255)
		
	)
AS
	BEGIN
		INSERT INTO Ingredient
			([IngredientName], [IngredientType], [IngredientDescription], [MeasurementType])
		VALUES
			(@IngredientName, @IngredientType, @IngredientDescription, @MeasurementType)
		SELECT SCOPE_IDENTITY()	
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_insert_ingredient_into_recipe ***'	
GO

CREATE PROCEDURE [dbo].[sp_insert_ingredient_into_recipe]
	(

		@RecipeID					[int],
		@IngredientID				[int],
	    @Quantity					[decimal](4,2)
		
	)
AS
	BEGIN
		INSERT INTO RecipeIngredient
			([RecipeID], [IngredientID], [Quantity])
		VALUES
			(@RecipeID, @IngredientID, @Quantity)
		SELECT SCOPE_IDENTITY()	
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_delete_ingredient_from_recipe ***'	
GO

CREATE PROCEDURE [dbo].[sp_delete_ingredient_from_recipe]
	(

		@RecipeID					[int],
		@IngredientID				[int],
	    @Quantity					[decimal](4,2)
		
	)
AS
	BEGIN
		DELETE FROM RecipeIngredient
		WHERE RecipeID = @RecipeID
		AND IngredientID = @IngredientID
		AND Quantity = @Quantity
		RETURN @@ROWCOUNT
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_delete_recipe_and_ingredients_from_recipe_by_recipe_id ***'	
GO

CREATE PROCEDURE [dbo].[sp_delete_recipe_and_ingredients_from_recipe_by_recipe_id]
	(

		@RecipeID					[int]
		
	)
AS
	BEGIN
		DELETE FROM RecipeIngredient
		WHERE RecipeID = @RecipeID
		
		DELETE FROM Recipe
		WHERE RecipeID = @RecipeID
		RETURN @@ROWCOUNT
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_insert_new_recipe_name ***'	
GO

CREATE PROCEDURE [dbo].[sp_insert_new_recipe_name]
	(

		@RecipeName				[nvarchar](255)
		
	)
AS
	BEGIN
		INSERT INTO Recipe
			([RecipeName])
		VALUES
			(@RecipeName)
		SELECT SCOPE_IDENTITY()	
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_update_passwordHash ***'	
GO

CREATE PROCEDURE [dbo].[sp_update_passwordHash]
	(

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

print '' print '*** creating sp_select_ingredient_quantity_by_recipe_id ***'	
GO

CREATE PROCEDURE [dbo].[sp_select_ingredient_quantity_by_recipe_id]
	(

		@RecipeID				[int]
		
	)
AS
	BEGIN
		SELECT RecipeIngredient.IngredientID, IngredientName, Quantity, MeasurementType, IngredientDescription
		FROM Ingredient JOIN RecipeIngredient
		ON Ingredient.IngredientID = RecipeIngredient.IngredientID
		JOIN Recipe
		ON RecipeIngredient.RecipeID = Recipe.RecipeID
		WHERE Recipe.RecipeID = @RecipeID
	END
GO


---------------------------------------------------------------------------------------------

print '' print '*** creating sp_select_all_recipes ***'	
GO

CREATE PROCEDURE [dbo].[sp_select_all_recipes]
AS
	BEGIN
		SELECT RecipeID, RecipeName
		FROM Recipe
		ORDER BY RecipeName ASC
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_select_all_ingredients ***'	
GO

CREATE PROCEDURE [dbo].[sp_select_all_ingredients]
AS
	BEGIN
		SELECT IngredientID, IngredientName, IngredientDescription, IngredientType, MeasurementType
		FROM Ingredient
		ORDER BY IngredientName ASC
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_delete_ingredient ***'	
GO

CREATE PROCEDURE [dbo].[sp_delete_ingredient]
	(
		@IngredientName				[nvarchar](50),
		@IngredientType				[nvarchar](50),
	    @IngredientDescription		[nvarchar](255),
		@MeasurementType			[nvarchar](255)
	)
AS
	BEGIN
		DELETE FROM Ingredient
		WHERE IngredientName = @IngredientName 
		AND IngredientType = @IngredientType
		AND IngredientDescription = @IngredientDescription
		AND MeasurementType = @MeasurementType
		RETURN @@ROWCOUNT
	END
GO

---------------------------------------------------------------------------------------------
-- Stored Procedures for Administrators
---------------------------------------------------------------------------------------------

print '' print '*** EMPLOYEE STORED PROCEDURES FOR ADMINS ***'	
GO

print '' print '*** creating sp_insert_new_mpuser ***'	
GO

CREATE PROCEDURE [dbo].[sp_insert_new_mpuser]
	(

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

CREATE PROCEDURE [dbo].[sp_select_all_mpusers_by_active]
	(

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

CREATE PROCEDURE [dbo].[sp_select_all_employee_by_id]
	(

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

CREATE PROCEDURE [dbo].[sp_reset_passwordHash]
	(

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

print '' print '*** creating sp_safely_deactivate_mpuser ***'	
GO

CREATE PROCEDURE [dbo].[sp_safely_deactivate_mpuser]
	(
		@MPUserID			[int]
	)
AS
	BEGIN
		DECLARE @Admins int
		
		SELECT @Admins = COUNT(RoleID)
		FROM MPUserRole
		WHERE RoleID = 'Admin'
		AND MPUserRole.MPUserID = @MPUserID
		
		IF @Admins = 1
			BEGIN
				RETURN 0
			END
		ELSE
			BEGIN
				UPDATE MPUser
					SET Active = 0
					WHERE MPUserID = @MPUserID
				RETURN @@ROWCOUNT	
			END
	END
GO


---------------------------------------------------------------------------------------------

print '' print '*** creating sp_reactivate_mpuser ***'	
GO

CREATE PROCEDURE [dbo].[sp_reactivate_mpuser]
	(

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

CREATE PROCEDURE [dbo].[sp_add_mpuser_role]
	(

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

CREATE PROCEDURE [dbo].[sp_safely_remove_mpuser_role]
	(

		@MPUserID			[int],
		@RoleID				[nvarchar](25)
		
	)
AS
	BEGIN
		DECLARE @Admins int
		
		SELECT @Admins = COUNT(RoleID)
		FROM MPUserRole
		WHERE RoleID = 'Admin'
		
		IF @RoleID = 'Admin' AND @Admins = 1
			BEGIN
				RETURN 0
			END
		ELSE
			BEGIN
				DELETE FROM MPUserRole
				WHERE MPUserID = @MPUserID
				AND RoleID = @RoleID
				RETURN @@ROWCOUNT
			END
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_select_all_mpuser_roles ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_mpuser_roles]
AS
	BEGIN
		SELECT RoleID
		FROM Role
		ORDER BY RoleID ASC
	END
GO

---------------------------------------------------------------------------------------------

print '' print '*** creating sp_update_mpuser_profile_data ***'	
GO

CREATE PROCEDURE [dbo].[sp_update_mpuser_profile_data]
	(
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

print '' print '*** creating sp_select_mpuser_by_email ***'	
GO

CREATE PROCEDURE [dbo].[sp_select_mpuser_by_email]
	(

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

CREATE PROCEDURE [dbo].[sp_select_mpuser_roles_by_mpuser_id]
	(

		@MPUserID			[int]	
		
	)
AS
	BEGIN
		SELECT RoleID
		FROM MPUserRole
		WHERE MPUserID = @MPUserID	
	END
GO


