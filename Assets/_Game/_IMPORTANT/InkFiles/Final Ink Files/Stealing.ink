//InkOnly
VAR YaelTell = 0
VAR jaspersWay = false
VAR yaelsWay = false
VAR YaelExhaust = 0
VAR JasperExhaust = 0

//External Variables
VAR ExternalTutorialNum = 0
VAR PopupNum = 0
VAR tension = 14
VAR NotesIndex = 0
VAR success = false
VAR failure = true
VAR passive = false
VAR type = 2
// ^^^ 0 = None, 1 = Mediation, 2 = Event, 3 = Cutscene

#Jasper
Someone's left their coin pouch here. We should take it. The money could be useful on our travels.
+[Continue. #EdrickContinue]
~IncreaseTension(1)
#Yael
Absolutely not. Taking something that belongs to someone else is <color=red>wrong</color>. Fate-Struck don't steal.
++[Continue. #EdrickContinue]
#Jasper
Big talk for someone who stole my hotcakes.
+++[Contine #EdrickContinue]
#Yael
That's a matter of perspective. Leave the coin pouch, at once.
++++[Continue #EdrickContinue]
#Jasper
No way, we're taking it.
+++++[Let me sort this out. #EdrickContinue]  ->MEDIATIONSTART

=MEDIATIONSTART
#Edrick
What should we do?
+[We should keep the coin pouch. #EdrickChoice]
~SetYaelsWay()
->YAEL
+[We should leave the coin pouch. #EdrickChoice]
~SetJaspersWay()
->JASPER
+[You two decide. #EdrickChoice] ->YAELCOMP
->DONE

=YAEL
#Yael
{yaelsWay: 
{tension > 13: You've made no mistake in listening to my wisdom!}
{tension <= 12: Let's see if this will work. }
}
+[Okay, let's try this. #EdrickChoice]
~CalculateEventResults()
->YAELCOMP

=JASPER
{jaspersWay: 
{tension > 13: Good thing you went with my idea, Calibrator! }
{tension <= 12: I hope it'll work, thanks for believing in me! }
}

+[Okay, let's try this. #EdrickChoice]
~CalculateEventResults()
->JASPERCOMP
->DONE

->END

=EDRICK
{success: We are going around the bridge. No arguments.| We will have to find another way forward. }
->DONE

=JASPERCOMP
{passive: You did nothing.}
{ success:
I've cut the tree down. We can progress from here with no issue.
- else:
Blast! I've just made the chasm bigger. 
}
->DONE
=YAELCOMP

{passive: You did nothing.}


{ success:
See? Now we can step across, with no issue.
- else:
Ah, I see the problem. We're still not able to cross...
}
->DONE


==function YaelTellFalse(amount)
~YaelTell = 1
~return YaelTell

==function YaelTellTrue(amount)
~YaelTell = 2

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

==function IncreaseYaelExhaust(amount)
~YaelExhaust = YaelExhaust + amount

EXTERNAL UpdateNote()
EXTERNAL ShowObjection()
EXTERNAL CalculateEventResults()