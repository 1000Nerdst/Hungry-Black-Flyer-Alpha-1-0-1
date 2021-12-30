from selenium import webdriver
from time import sleep
from bs4 import BeautifulSoup
import requests
#import mysql 
import pandas as pd
import pydoc
import xml.etree.ElementTree as xml
import os
import shutil
import csv
import sys


def getServingOptions(chosenUrl, conversionUnit):

    gramServingMultiper = conversionUnit

    result = requests.get(chosenUrl)
    src = result.content

    soup = BeautifulSoup(src, 'lxml')

    allDivs = soup.find_all("div")
    indexOfDiv = 0
    listOfTables = ["","","","",""]

    for div in allDivs:
        divAttrs = div.attrs

		#targetClass = "col-md-4 col-xs-12 no-padding"
        targetClass = ["col-md-4", "col-xs-12", "no-padding"]
        classStr = 'class'

        tableIndex = -1
        classIndex = 0
        keys = divAttrs.keys()
        listOfValues = list(divAttrs.values())
        for key in keys:
            if(classStr == key):

                classValue = listOfValues[classIndex]
                #classValue = classValue[0]
                if(classValue == targetClass):

                    contents = div.contents

                    for content in contents:
                        if(content == "\n"):
                            contentAttrs = ""
                        else:
                            contentAttrs = content.attrs
                            tableIndex +=1

                        targetContentAttr = {'class': ['label2']}
                        if (contentAttrs == targetContentAttr):
                            listOfTables[tableIndex] = content
            classIndex += 1

    nutrionFactsClass = listOfTables[0]
    vitiaminsClass = listOfTables[1]
    mineralsClass = listOfTables[2]
    aminoAcidsClass = listOfTables[3]
    fattyAcidsClass = listOfTables[4]

    nutritionArray = readNutritionFacts(nutrionFactsClass, gramServingMultiper)
    vitaminArray = readVitamins(vitiaminsClass, gramServingMultiper)
    mineralArray = readMinerals(mineralsClass, gramServingMultiper)
    readAminoAcids(aminoAcidsClass, gramServingMultiper)
    fattyAcidArray = readFattyAcids(fattyAcidsClass, gramServingMultiper)

    saveServerAndCVS(nutritionArray, vitaminArray, mineralArray, fattyAcidArray)

def readNutritionFacts(nutrionFactsClass, gramServingMultiper):
    standardCalories = 0
    standardTotalFat = 0
    standardTotalCarbohydrate = 0
    standardTotalProtien = 0
    standardCholesterol = 0
    standardTransFat = 0
    standardSugar = 0
    standardSatFat = 0
    standardFiber = 0

    allTag = nutrionFactsClass.contents 

    #caloriesClassAttr 
    allMacrosList = ['daily-value', 'sm-text']

    for tag in allTag:
        if(tag != "\n"):
            tagAttr = tag.attrs
            tagAttrKeys = tagAttr.keys()
            tagAttrValuesList = list(tag.attrs.values())

            if (tagAttrValuesList == []):
                tagAttrValuesList = [0, 1]

            #find the calories
            if(tagAttrValuesList[0] == ['calories-info']):
                caloriesTagInfo = list(tag.contents)

                caloreisTag = list(caloriesTagInfo[3].contents)

                finalCalorieTagContent = list(caloreisTag[1].contents)

                standardCalories = float(finalCalorieTagContent[0])

            if(tagAttrValuesList[0] == allMacrosList):
                allMacrosInfro = list(tag.contents)

                totalFatTag = list(allMacrosInfro[5].contents)
                finalFatTagContent = list(totalFatTag[2].contents)
                standardTotalFat = float(finalFatTagContent[0])

                totalCarbohydrateTag = list(allMacrosInfro[19].contents)
                finalCarbohydrateTagContent = list(totalCarbohydrateTag[2].contents)
                standardTotalFat = float(finalCarbohydrateTagContent[0])

                totalProtienTag = list(allMacrosInfro[29].contents)
                finalProtienTagContent = list(totalProtienTag[2].contents)
                standardTotalProtien = float(finalProtienTagContent[0])

                cholesterolTag = list(allMacrosInfro[15].contents)
                finalcholesterolTagContent = list(cholesterolTag[2].contents)
                if(finalcholesterolTagContent[0].isnumeric()):
                    standardCholesterol = float(finalcholesterolTagContent[0])

                transFatTag = list(allMacrosInfro[11].contents)
                transFatTagContent = list(transFatTag[2].contents)
                if(transFatTagContent[0].isnumeric()):
                    standardTransFat = float(transFatTagContent[0])

                sugarTag = list(allMacrosInfro[25].contents)
                sugarTagContent = list(sugarTag[1].contents)
                if(sugarTagContent[0].isnumeric()):
                    standardSugar = float(sugarTagContent[0])
                

                satFatTag = list(allMacrosInfro[7].contents)
                satFatTagContent = list(satFatTag[1].contents)
                standardSatFat = float(satFatTagContent[0])

                fiberTag = list(allMacrosInfro[21].contents)
                finalFiberTagContent = list(fiberTag[1].contents)
                standardFiber = float(finalFiberTagContent[0])
        else:
            tagAttr = ""

    #this is where I need to convert the values and put them into a list or array
    chosenCalories = (standardCalories * gramServingMultiper)/100
    chosenTotalFat = (standardTotalFat * gramServingMultiper)/100
    chosenTotalCarbohydrate = (standardTotalCarbohydrate * gramServingMultiper)/100
    chosenTotalProtien = (standardTotalProtien * gramServingMultiper)/100
    chosenCholesterol = (standardCholesterol * gramServingMultiper)/100
    chosenTransFat = (standardTransFat * gramServingMultiper)/100
    chosenSugar = (standardSugar * gramServingMultiper)/100
    chosenSatFat = (standardSatFat * gramServingMultiper)/100
    chosenFiber = (standardFiber * gramServingMultiper)/100

    macroArray = [chosenCalories, chosenTotalFat, chosenTotalCarbohydrate, chosenTotalProtien, chosenCholesterol, chosenTransFat, chosenSugar, chosenSatFat, chosenFiber]

    return macroArray
    

def readVitamins(vitiaminsClass, gramServingMultiper):
    standardVitaminA = 0
    standardVitaminB1 = 0
    standardVitaminB2 = 0
    standardVitaminB3 = 0
    standardVitaminB5 = 0
    standardVitaminB6 = 0
    standardVitaminB7 = 0
    standardVitaminB9 = 0
    standardVitaminB12 = 0
    standardVitaminC = 0
    standardVitaminD = 0
    standardVitaminE = 0
    standardVitaminK = 0
    standardCholine = 0

    allVitiaminsTag = list(vitiaminsClass.contents)
    vitiaminsTag = list(allVitiaminsTag[1].contents)

    listedVitiamins = vitiaminsTag[5]
    i = 0
    for vitiamin in listedVitiamins:
        if(vitiamin != "\n"):
            vitiaminList = list(vitiamin.contents)
            vitiaminString = vitiaminList[0]

            if(vitiaminString == 'Vitamin A '):
                vitiaminValue = list(vitiaminList[1].contents)
                standardVitaminA = float(vitiaminValue[0])

            if(vitiaminString == 'Vitamin B1 (Thiamin) '):
                vitiaminValue = list(vitiaminList[1].contents)
                standardVitaminB1 = float(vitiaminValue[0])

            if(vitiaminString == 'Vitamin B2 (Riboflavin) '):
                vitiaminValue = list(vitiaminList[1].contents)
                standardVitaminB2 = float(vitiaminValue[0])

            if(vitiaminString == 'Vitamin B3 (Niacin) '):
                vitiaminValue = list(vitiaminList[1].contents)
                standardVitaminB3 = float(vitiaminValue[0])

            if(vitiaminString == 'Vitamin B5 (Pantothenic acid) '):
                vitiaminValue = list(vitiaminList[1].contents)
                standardVitaminB5 = float(vitiaminValue[0])

            if(vitiaminString == 'Vitamin B6 '):
                vitiaminValue = list(vitiaminList[1].contents)
                standardVitaminB6 = float(vitiaminValue[0])

            if(vitiaminString == 'Vitamin B7 '):
                vitiaminValue = list(vitiaminList[1].contents)
                standardVitaminB7 = float(vitiaminValue[0])

            if(vitiaminString == 'Vitamin B9 '):
                vitiaminValue = list(vitiaminList[1].contents)
                standardVitaminB9 = float(vitiaminValue[0])

            if(vitiaminString == 'Vitamin B12 '):
                vitiaminValue = list(vitiaminList[1].contents)
                standardVitaminB12 = float(vitiaminValue[0])

            if(vitiaminString == 'Vitamin C '):
                vitiaminValue = list(vitiaminList[1].contents)
                standardVitaminC = float(vitiaminValue[0])

            if(vitiaminString == 'Vitamin D '):
                vitiaminValue = list(vitiaminList[1].contents)
                standardVitaminD = float(vitiaminValue[0])

            if(vitiaminString == 'Vitamin E '):
                vitiaminValue = list(vitiaminList[1].contents)
                standardVitaminE = float(vitiaminValue[0])

            if(vitiaminString == 'Vitamin K '):
                vitiaminValue = list(vitiaminList[1].contents)
                standardVitaminK = float(vitiaminValue[0])

            if(vitiaminString == 'Choline '):
                vitiaminValue = list(vitiaminList[1].contents)
                standardCholine = float(vitiaminValue[0])

    #this is where I need to convert the values and put them into a list or array
    chosenVitaminA = (standardVitaminA * gramServingMultiper)/100
    chosenVitaminB1 = (standardVitaminB1 * gramServingMultiper)/100
    chosenVitaminB2 = (standardVitaminB2 * gramServingMultiper)/100
    chosenVitaminB3 = (standardVitaminB3 * gramServingMultiper)/100
    chosenVitaminB5 = (standardVitaminB5 * gramServingMultiper)/100
    chosenVitaminB6 = (standardVitaminB6 * gramServingMultiper)/100
    chosenVitaminB7 = (standardVitaminB7 * gramServingMultiper)/100
    chosenVitaminB9 = (standardVitaminB9 * gramServingMultiper)/100
    chosenVitaminB12 = (standardVitaminB12 * gramServingMultiper)/100
    chosenVitaminC = (standardVitaminC * gramServingMultiper)/100
    chosenVitaminD = (standardVitaminD * gramServingMultiper)/100
    chosenVitaminE = (standardVitaminE * gramServingMultiper)/100
    chosenVitaminK = (standardVitaminK * gramServingMultiper)/100
    chosenCholine = (standardCholine * gramServingMultiper)/100

    vitaminArray = [chosenVitaminA, chosenVitaminB1, chosenVitaminB2, chosenVitaminB3, chosenVitaminB5, chosenVitaminB6, chosenVitaminB7, chosenVitaminB9, chosenVitaminB12, chosenVitaminC, chosenVitaminD, chosenVitaminE, chosenVitaminK, chosenCholine]

    return vitaminArray

def readMinerals(mineralsClass, gramServingMultiper):
    standardCalcium = 0
    standardChloride = 0
    standardChromium = 0
    standardCopper = 0
    standardFluoride = 0
    standardIodine = 0
    standardIron = 0
    standardMagnesium = 0
    standardManganese = 0
    standardMolybdenum = 0
    standardPhosphorus = 0
    standardPotassium = 0
    standardSelenium = 0
    standardSodium = 0
    standardSulfur = 0
    standardZinc = 0

    allMineralsTag = list(mineralsClass.contents)
    mineralsTag = list(allMineralsTag[1].contents)

    listedMinerals = mineralsTag[5]

    for mineral in listedMinerals:
        if(mineral != "\n"):
            mineralList = list(mineral.contents)
            mineralString = mineralList[0]

            if(mineralString == 'Calcium '):
                mineralValue = list(mineralList[1].contents)
                standardCalcium = float(mineralValue[0])

            if(mineralString == 'Chloride '):
                mineralValue = list(mineralList[1].contents)
                standardChloride = float(mineralValue[0])

            if(mineralString == 'Chromium '):
                mineralValue = list(mineralList[1].contents)
                standardChromium = float(mineralValue[0])

            if(mineralString == 'Copper '):
                mineralValue = list(mineralList[1].contents)
                standardCopper = float(mineralValue[0])

            if(mineralString == 'Fluoride '):
                mineralValue = list(mineralList[1].contents)
                standardFluoride = float(mineralValue[0])

            if(mineralString == 'Iodine '):
                mineralValue = list(mineralList[1].contents)
                standardIodine = float(mineralValue[0])

            if(mineralString == 'Iron '):
                mineralValue = list(mineralList[1].contents)
                standardIron = float(mineralValue[0])

            if(mineralString == 'Magnesium '):
                mineralValue = list(mineralList[1].contents)
                standardMagnesium = float(mineralValue[0])

            if(mineralString == 'Manganese '):
                mineralValue = list(mineralList[1].contents)
                standardMagnesium = float(mineralValue[0])

            if(mineralString == 'Molybdenum '):
                mineralValue = list(mineralList[1].contents)
                standardMolybdenum = float(mineralValue[0])

            if(mineralString == 'Phosphorus '):
                mineralValue = list(mineralList[1].contents)
                standardPhosphorus = float(mineralValue[0])

            if(mineralString == 'Potassium '):
                mineralValue = list(mineralList[1].contents)
                standardPotassium = float(mineralValue[0])

            if(mineralString == 'Selenium '):
                mineralValue = list(mineralList[1].contents)
                standardSelenium = float(mineralValue[0])

            if(mineralString == 'Sodium '):
                mineralValue = list(mineralList[1].contents)
                standardSodium = float(mineralValue[0])

            if(mineralString == 'Sulfur '):
                mineralValue = list(mineralList[1].contents)
                standardSulfur = float(mineralValue[0])
            
            if(mineralString == 'Zinc '):
                mineralValue = list(mineralList[1].contents)
                standardZinc = float(mineralValue[0])

    #this is where I need to convert the values and put them into a list or array
    chosenCalcium = (standardCalcium * gramServingMultiper)/100
    chosenChloride = (standardChloride * gramServingMultiper)/100
    chosenChromium = (standardChromium * gramServingMultiper)/100
    chosenCopper = (standardCopper * gramServingMultiper)/100
    chosenFluoride = (standardFluoride * gramServingMultiper)/100
    chosenIodine = (standardIodine * gramServingMultiper)/100
    chosenIron = (standardIron * gramServingMultiper)/100
    chosenMagnesium = (standardMagnesium * gramServingMultiper)/100
    chosenManganese = (standardManganese * gramServingMultiper)/100
    chosenMolybdenum = (standardMolybdenum * gramServingMultiper)/100
    chosenPhosphorus = (standardPhosphorus * gramServingMultiper)/100
    chosenPotassium = (standardPotassium * gramServingMultiper)/100
    chosenSelenium = (standardSelenium * gramServingMultiper)/100
    chosenSodium = (standardSodium * gramServingMultiper)/100
    chosenSulfur = (standardSulfur * gramServingMultiper)/100
    chosenZinc = (standardZinc * gramServingMultiper)/100

    mineralArray = [chosenCalcium, chosenChloride, chosenChromium, chosenCopper, chosenFluoride, chosenIodine, chosenIron, chosenMagnesium, chosenManganese, chosenMolybdenum, chosenPhosphorus, chosenPotassium, chosenSelenium, chosenSodium, chosenSulfur, chosenZinc]

    return mineralArray

def readAminoAcids(aminoAcidsClass, gramServingMultiper):
    temp = 0

def readFattyAcids(fattyAcidsClass, gramServingMultiper):
    standardOmega3 = 0
    standardOmega6 = 0
    standardALA = 0
    standardEPA = 0
    standardDPA = 0
    standardDHA = 0

    allFattyAcidsTag = list(fattyAcidsClass.contents)
    fattyAcidsTag = list(allFattyAcidsTag[1].contents)

    listedFattyAcids = fattyAcidsTag[5]

    for fattyAcid in listedFattyAcids:
        if(fattyAcid != "\n"):
            fattyAcidList = list(fattyAcid.contents)
            fattyAcidString = fattyAcidList[0]

            if(fattyAcidString == 'Omega-3 '):
                fattyAcidValue = list(fattyAcidList[1].contents)
                standardOmega3 = float(fattyAcidValue[0])

            if(fattyAcidString == 'Chloride '):
                fattyAcidValue = list(fattyAcidList[1].contents)
                standardOmega6 = float(fattyAcidValue[0])

            if(fattyAcidString == 'EPA '):
                fattyAcidValue = list(fattyAcidList[1].contents)
                standardEPA = float(fattyAcidValue[0])

            if(fattyAcidString == 'DPA '):
                fattyAcidValue = list(fattyAcidList[1].contents)
                standardDPA = float(fattyAcidValue[0])

            if(fattyAcidString == 'DHA '):
                fattyAcidValue = list(fattyAcidList[1].contents)
                standardDHA = float(fattyAcidValue[0])

            if(fattyAcidString == 'ALA '):
                fattyAcidValue = list(fattyAcidList[1].contents)
                standardALA = float(fattyAcidValue[0])

    #this is where I need to convert the values and put them into a list or array
    chosenOmega3 = (standardOmega3 * gramServingMultiper)/100
    chosenOmega6 = (standardOmega6 * gramServingMultiper)/100
    chosenALA = (standardALA * gramServingMultiper)/100
    chosenEPA = (standardEPA * gramServingMultiper)/100
    chosenDPA = (standardDPA * gramServingMultiper)/100
    chosenDHA = (standardDHA * gramServingMultiper)/100

    fattyAcidArray = [chosenOmega3, chosenOmega6, chosenALA, chosenEPA, chosenDPA, chosenDHA]

    return fattyAcidArray

def saveServerAndCVS(nutritionArray, vitaminArray, mineralArray, fattyAcidArray):
    
    #cnxn = pyodbc.connect(cnxn_str)
    #df = pd.read_sql("SELECT * FROM Table1", cnxn)

    fileName = "newIngredent.xml"

    #read all the values from arrays

    print(nutritionArray[0])
    print(nutritionArray[1])
    print(nutritionArray[2])
    print(nutritionArray[3])
    print(nutritionArray[4])
    print(nutritionArray[5])
    print(nutritionArray[6])
    print(nutritionArray[7])
    print(nutritionArray[8])
    print(vitaminArray[0])
    print(vitaminArray[1])
    print(vitaminArray[2])
    print(vitaminArray[3])
    print(vitaminArray[4])
    print(vitaminArray[5])
    print(vitaminArray[6])
    print(vitaminArray[7])
    print(vitaminArray[8])
    print(vitaminArray[9])
    print(vitaminArray[10])
    print(vitaminArray[11])
    print(vitaminArray[12])
    print(vitaminArray[13])
    print(mineralArray[0])
    print(mineralArray[1])
    print(mineralArray[2])
    print(mineralArray[3])
    print(mineralArray[4])
    print(mineralArray[5])
    print(mineralArray[6])
    print(mineralArray[7])
    print(mineralArray[8])
    print(mineralArray[9])
    print(mineralArray[10])
    print(mineralArray[11])
    print(mineralArray[12])
    print(mineralArray[13])
    print(mineralArray[14])
    print(mineralArray[15])
    print(fattyAcidArray[0])
    print(fattyAcidArray[1])
    print(fattyAcidArray[2])
    print(fattyAcidArray[3])
    print(fattyAcidArray[4])
    print(fattyAcidArray[5])
    
def readXMLFile():
    fileName = "C:/Users/solow/OneDrive/Desktop/Projects/Hungry Flyer/dietNerdAlpha 1.0.1/dietNerdAlpha 1.0.1/dietNerdAlpha 1.0.1/Config Files/newIngredientInformation.csv"
    rows = []

    with open(fileName, 'r') as csvfile:
        csvreader = csv.reader(csvfile)

        for row in csvreader:
            rows.append(row)

    return rows

#tempUrl = "https://nutrientoptimiser.com/nutritional-value-egg-whole-raw-fresh/"
rows = readXMLFile()
information = rows[1]
conversion = float(information[1])
getServingOptions(information[0], conversion)