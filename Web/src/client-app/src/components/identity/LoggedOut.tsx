import React from "react";
import { useNavigate } from 'react-router';

export type LoggedOutProps = {
  message?: string;
  returnUrl?: string;
}

function LoggedOut(props: LoggedOutProps) {
  const navigate = useNavigate();

  React.useEffect(() => {
    let timeoutId: NodeJS.Timeout | null = setTimeout(() => {
      timeoutId = null;
      navigate(props.returnUrl ?? "/")
    })

    return () => {
      if (timeoutId !== null) {
        clearTimeout(timeoutId);
      }
    }
  }, [navigate, props.returnUrl]);

  return (
    <div>{props.message}</div>
  )
}

export default React.memo(LoggedOut);