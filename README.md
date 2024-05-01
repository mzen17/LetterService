# Letter Service
- Estimated Time: 1-3 hours ~2 hours

## Code Planning (15min, tracked)
- Input: CombinedLetters root folder, as well as day to process
- Get CombinedLetter Folder
- Go to Inputfiles
- Go to Admission and Scholarship, keep list of everything in them (8 digit #s)
- *Archive* | For each one of these (in admissions and scholarships), recreate the file in the archive folder under a date (input), and under the folder depending on what the list is (admission or scholarship). If the folder for date exists for some reason, delete it. 
- *Combine* | Run through both of these lists using a nested for loop. If one item equals another, them we have a case to merge. Copy the content of admission.txt and scholarship.txt, and create a new file in Output with folder of the date (if it already exists, delete). Add this number to a list.
- *Analysis* | Print out length of list specified in previous step, as well as the content of list.
- From main program, find all dates possible from Admission and Scholarship, and run the process.

## Code Sketching (30 min, tracked)
Basic outline of code and skeleton methods.

## Test Setup (5 min, tracked)
Basic tests

## Code Implementation & Debug (60 + 15 min, tracked)
Implemented all phases except [From main program, find all dates possible from Admission and Scholarship, and run the process].

## Feature: Automatic date handling Implementation & Debug (15 + 5 min, tracked)
Automatic date collection (Date collection from Union)

## Code Documentation and Refactoring (15 min)

## Final Code Review and Testing 

# Misc
- Bundled the other functions (archive and text report) as part of LetterService class as it is reasonable to think that it should handle every part of the pipeline consisting of letters.