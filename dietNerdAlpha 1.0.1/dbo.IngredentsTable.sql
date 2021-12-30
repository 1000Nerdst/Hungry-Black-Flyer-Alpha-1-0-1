﻿CREATE TABLE [dbo].[ingrentsTable]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [IngredentName] NCHAR(40) NOT NULL, 
    [StdServingSize] NCHAR(40) NOT NULL, 
    [Calories] FLOAT NOT NULL, 
    [Fats_G] FLOAT NOT NULL, 
    [Carbs_G] FLOAT NOT NULL, 
    [Protein_G] FLOAT NULL, 
    [Cholesterol_mg] FLOAT NULL, 
    [VitaminA_IU] FLOAT NULL, 
    [VitaminB1_mg] FLOAT NULL, 
    [VitaminB2_mg] FLOAT NULL, 
    [VitaminB3_mg] FLOAT NULL, 
    [VitaminB5_mg] FLOAT NULL, 
    [VitaminB6_mg] FLOAT NULL, 
    [VitaminB7_ug] FLOAT NULL, 
    [VitaminB9_ug] FLOAT NULL, 
    [VitaminB12_ug] FLOAT NULL, 
    [Vitaminc_mg] FLOAT NULL, 
    [VitaminD_IU] FLOAT NULL, 
    [VitaminE_mg] FLOAT NULL, 
    [VitaminK_ug] FLOAT NULL, 
    [Choline_mg] FLOAT NULL, 
    [Calcium_mg] FLOAT NULL, 
    [Chloride_g] FLOAT NULL, 
    [Chromium_mcg] FLOAT NULL, 
    [Fluoride_mg] FLOAT NULL, 
    [Iodine_ug] FLOAT NULL, 
    [Iron_mg] FLOAT NULL, 
    [Magnesium_mg] FLOAT NULL, 
    [Manganese_mg] FLOAT NULL, 
    [Molybdenum_ug] FLOAT NULL, 
    [Phosphorus_mg] FLOAT NULL, 
    [Potassium_g] FLOAT NULL, 
    [Selenium_ug] FLOAT NULL, 
    [Sodium_mg] FLOAT NULL, 
    [Sulfur_mg] FLOAT NULL, 
    [Zinc_mg] FLOAT NULL, 
    [Omega3_g] FLOAT NULL, 
    [Omega6_g] FLOAT NULL, 
    [ALA_g] FLOAT NULL, 
    [EPA_g] FLOAT NULL, 
    [DPA_g] FLOAT NULL, 
    [DHA_g] FLOAT NULL, 
    [SaturatedFat_g] FLOAT NULL, 
    [TransFat_g] FLOAT NULL, 
    [Fiber_g] FLOAT NULL, 
    [Sugars_g] FLOAT NULL
)
