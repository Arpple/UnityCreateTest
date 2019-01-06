# UnityCreateTest
create a test script file in the same directory structure as source file for unity project

I have a folder structure like this
- Assets
  - Scripts
    - ...someScript.cs
  - Editor
    - Test
      - ...someScriptTest.cs

when I want to create a test for a script located in deep nested directory it is really >:(

# Usage
- right click on the script you want to create test for
- select `Create Test`
- it will create the new test script with same directory structure in test root
- and also *select in inspector* for you > you can click open to start writing test

# More Todo...
- add ability to edit `SCRIPT_ROOT`, `TEST_ROOT`
- custome template
- set selection of asset window to the created file (I tried but can't find the way to do it)
