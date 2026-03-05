//InkOnly

VAR jaspersWay = false
VAR yaelsWay = false


//External Variables
VAR ExternalTutorialNum = 0
VAR PopupNum = 0
VAR tension = 14
VAR NotesIndex = 0
VAR success = false
//VAR failure = false
VAR passive = false
VAR type = 3
// ^^^ 0 = None, 1 = Mediation, 2 = Event, 3 = Cutscene

#Jasper
What's on your mind? 

->DONE

=YAEL
#Edrick
+[How do you feel about Yael so far? #EdrickChoice]

->DONE
=HERSELF
What do you do in your free time?
What kind of things do you like?
Do you miss anything in the city?
->DONE
=START
Are you prepared for the Ritual?
How did you feel when you were chosen for this mission?
->DONE
==function IncreaseTension(amount)
~tension = tension + amount

==function DecreaseTension(amount)
~tension = tension - amount

==function SetJaspersWay
~jaspersWay = true

==function SetYaelsWay
~yaelsWay = true

==function ChangeNotesIndex(amount)
~NotesIndex = amount

==function ExtendedTutorial(amount)
~ExternalTutorialNum = amount

==function UpdatePopupNumber(amount)
~PopupNum = amount




EXTERNAL UpdateNote()
EXTERNAL ShowObjection()
EXTERNAL CalculateEventResults()