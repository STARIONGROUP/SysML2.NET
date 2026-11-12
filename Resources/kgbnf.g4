// Grammar

grammar kgbnf;

specification : (NL)* rule_definition+ EOF ;

rule_definition
    : name=GRAPHICAL_NAME op=(ASSIGN | PARTIAL_ASSIGN) rule_body=alternatives NL+
    ;

alternatives
    : alternative (PIPE alternative)*
    ;

alternative
    : element*
    ;

element
    : group
    | terminal
    | image
    | edge_end
    | non_terminal
    ;

group
    : LPAREN alternatives RPAREN (suffix=suffix_op)?
    ;

terminal
    : val=SINGLE_QUOTED_STRING (suffix=suffix_op)?
    ;

image
    : IMAGE
    ;

edge_end
    : AMPERSAND name=GRAPHICAL_NAME (suffix=suffix_op)?
    ;

non_terminal
    : name=(GRAPHICAL_NAME | TEXTUAL_NAME) (suffix=suffix_op)?
    ;

suffix_op : '*' | '+' | '?' ;

// Lexer
PARTIAL_ASSIGN : '=|' | '|=' ;
ASSIGN : '=' ;
PIPE : '|' ;
LPAREN : '(' ;
RPAREN : ')' ;
AMPERSAND : '&' ;
IMAGE : '<img' ~[>]* '>' ;

GRAPHICAL_NAME : [a-z]+ ('-' [a-z]+)* ;
TEXTUAL_NAME : [a-zA-Z_] [a-zA-Z0-9_]* ;
SINGLE_QUOTED_STRING : '\'' (~['\\] | '\\' .)* '\'' ;

COMMENT : '//' ~[\r\n]* -> skip ;
WS : [ \t]+ -> skip ;
CONTINUATION : '\r'? '\n' [ \t]+ -> skip ;
NL : '\r'? '\n' ;
