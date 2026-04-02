//InkOnly
VAR YaelTell = 0
VAR jasperVocab = false
VAR yaelRitual = false
VAR YaelExhaust = 0
VAR JasperExhaust = 0

//External Variables
VAR ExternalTutorialNum = 0
VAR PopupNum = 0
VAR tension = 5
VAR NotesIndex = 0
VAR success = false
VAR failure = true
VAR passive = false
VAR type = 2
// ^^^ 0 = None, 1 = Mediation, 2 = Event, 3 = Cutscene

#Edrick
There is not much time, friends.
+[Continue #EdrickContinue]
#Jasper
Who said we were friends?
~IncreaseTension(5)
++[Okay... comrades. #EdrickContinue]
#Yael
You can take us there, right Edrick? To the Temple of Scales?
+++[I can take you there. #EdrickChoice]
#YaelPensive
You’re going to have to. The sooner we get there, the better... let's look at the map.
+++[Only if you two stop bickering. #EdrickChoice]
#Jasper
I have complete faith in you, dear Calibrator! Even if Yael yaps my ears off along the way! Now let's take a look at the map.
++++[Let's do so. #EdrickContinue]
#Yael
I heard that.
->END

==function IncreaseTension(amount)
~tension = tension + amount
