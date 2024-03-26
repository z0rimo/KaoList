import React from "react";

interface IHighlightMatch {
  text?: string;
  query?: string;
}

function HighlightMatch({ text, query }: IHighlightMatch) {
  if (!text) return null;
  if (!query) return <span>{text}</span>;

  const parts = text.split(new RegExp(`(${query})`, 'gi'));

  return (
    <>
      {parts.map((part, index) =>
        part.toLowerCase() === query.toLowerCase() ? (
          <span key={index} style={{ backgroundColor: '#BEFAD7' }}>{part}</span>
        ) : (
          <span key={index}>{part}</span>
        )
      )}
    </>
  );
}

export default React.memo(HighlightMatch);