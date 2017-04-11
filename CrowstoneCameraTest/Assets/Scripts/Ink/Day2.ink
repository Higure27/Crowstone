CONST Currency = 0
VAR day2_Complete = false
VAR wanted_Poster = false
VAR showed_Poster_Kathrine = false



->Harriet
===Pemberton===
Good morning, Sheriff. I'm glad to see you're an early riser. I have a new lead for you to follow. There's a man in town, Jacob Morgan. I believe he's caught wind of my presence. ->converstation_options
=converstation_options
*[Have any tips on how I should handle him?]
    He seems a hard man, so I would suggest that in addition to trying to persuade him, you could try the route of blackmail. I'll bet he has some dirt on him. He seems the type. Of course, you're always welcome to try the other options we discussed yesterday. -> converstation_options
+[I'd best get to work, then.]
    ->DONE


===Jacob_Morgen===
What the hell do you want, Sheriff? Can't a man walk the streets without a lawman riding his coat tails.
* [I hear you've been poking your nose into business that isn't yours, Jacob.] The man in the red hat...I need you to stop talking about him.
        And why should I listen to you? Who cares if it's my business or not? It's a free country, last I checked. I can talk about anybody I want to.
        **[I'll be back to continue this conversation later.]
            ->DONE

            
===Harriet===
Well, good day to you, Sheriff. What can I do you for?
+[Nothing for now. have a good day Miss.]
    ->DONE
+[What can you tell me about Jacob Morgan?]
    Jacob Morgan? Oh, yes! He's the new man in town, right? The rugged fellow. I know a little bit, Sheriff. What would you like to know about him? I'll see if I can help. ->investigate
=investigate
    
*[What is he like?]
        Hard to tell, really. He's a firm man, and he rarely smiles. I've noticed he looks an awful lot like one of the wanted posters you have hanging up on your wall. 'Black Jack?' But surely that can't be him.... Is there anything else you need to know?->investigate
*[Do you know who he associates with in town?]
        Between you, me, and the tree, Sheriff, I've seen him over at Katherine Blakley's home. I think he fancies her. Isn't young love sweet? What else can I do you for, Sheriff?->investigate
+[Nothing for now. have a good day Miss.]
    ->DONE
    
    
===Katherine_Blakely===
Good day, Sheriff! You don't make your way out this way too often. What can I do for you?

*{wanted_Poster}[I understand you've been seeing Jacob Morgan]
    Wh-what is this...? A wanted poster? With Jacob on it? This can't be...I thought I knew him. I need to speak to him right now. Thank you, Sheriff. ->DONE
    
*[Nothing for now Ms. Blakley]
    ->DONE
    

