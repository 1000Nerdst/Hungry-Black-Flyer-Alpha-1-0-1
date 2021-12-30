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


def getServingOptions(chosenUrl):
    result = requests.get(chosenUrl)
    src = result.content

    soup = BeautifulSoup(src, 'lxml')

    #read the option of gram sizes
    servingSizeControlPad = soup.find_all("option")
    length = 0
    for servings in servingSizeControlPad:
        length += 1

    valueStringList = [""] * length
    valueIntList = [0] * length

    servingOption = 0
    numberOfOptions = 0
    for servings in servingSizeControlPad:
        numberOfOptions +=1

    fileName = "servingOptions.csv"

    optionNumber = 0
    for servings in servingSizeControlPad:
        optionNumber += 1
    
    print(optionNumber)

    fields = ["Standard Serving Size", "Ratio"]
    rows= []

    for servings in servingSizeControlPad:
        servingsContent = servings.contents
        contentName = servingsContent[0]
        servingAttrs = servings.attrs

        i = 0

        keys = servingAttrs.keys()
        values = servingAttrs.values()
        keyList = list(keys)
        valuesList = list(values)
        for key in keys:
            valueStr = 'value'
            if(valueStr == key):
                value = valuesList[i]

            i+=1

        valueStringList[servingOption] = contentName
        valueIntList[servingOption] = float(value)
        indiviualLine = [contentName, value]
        contentName = contentName.replace(", ", " ")
        print(contentName)
        print(value)
        rows.append(indiviualLine)
        servingOption += 1

    with open(fileName, 'w') as csvfile:
        csvWriter = csv.writer(csvfile)

        csvWriter.writerow(fields)
        csvWriter.writerows(rows)
        #print(rows)

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
getServingOptions(information[0])