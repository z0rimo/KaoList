import React from "react";
import ClassNameHelper from "../ClassNameHelper";

type DropdownClickPropsType = {
  activeAction?: 'click';
  onExpand?: React.HTMLAttributes<HTMLDivElement>['onClick'];
  onUnexpand?: (evt: MouseEvent) => void;
}

type DropdownHoverPropsType = {
  activeAction?: 'hover';
  onExpand?: React.HTMLAttributes<HTMLDivElement>['onMouseEnter'];
  onUnexpand?: (evt: MouseEvent) => void;
}

type DropdownBasePropsType = (React.HTMLAttributes<HTMLDivElement> & {
  children: [React.ReactElement, React.ReactElement];
  expanded?: boolean;
})

type DropdownPropsType = DropdownBasePropsType & (DropdownHoverPropsType | DropdownClickPropsType)

interface IActiveActionNameDict {
  "click": "onClick",
  "hover": "onMouseEnter"
}

const activeActionNameDict: IActiveActionNameDict = {
  "click": "onClick",
  "hover": "onMouseEnter"
}

interface IDisableActionNameDict {
  "click": "click",
  "hover": "mouseout"
}

const disableActionNameDict: IDisableActionNameDict = {
  "click": "click",
  "hover": "mouseout"
}

const Dropdown = React.forwardRef<HTMLDivElement, DropdownPropsType>(({
  activeAction,
  children,
  className,
  expanded,
  onExpand,
  onUnexpand,
  ...rest
}, ref) => {
  const classNames = React.useMemo(
    () => ClassNameHelper.concat('dropdown', className),
    [className]
  );

  const activeActionName = React.useMemo(
    () => activeActionNameDict[activeAction!],
    [activeAction]
  );

  const disableActionName = React.useMemo(
    () => disableActionNameDict[activeAction!],
    [activeAction]
  );

  const head = children[0].type === React.Fragment ? <div>{children[0]}</div> : children[0];
  const propsAction = head?.props[activeActionName];
  const handleExpand = React.useCallback<React.MouseEventHandler<HTMLDivElement>>(evt => {
    propsAction && propsAction(evt);
    if (evt.isPropagationStopped()) {
      return;
    }

    onExpand!(evt);
  }, [onExpand, propsAction]);

  React.useEffect(() => {
    if (!expanded) {
      return;
    }

    document.addEventListener(disableActionName, onUnexpand!);
    return () => {
      document.removeEventListener(disableActionName, onUnexpand!);
    }
  }, [expanded, onUnexpand, disableActionName]);

  const headWrapper = head && React.cloneElement(
    head,
    {
      ...head?.props,
      [activeActionName]: handleExpand
    }
  )

  return (
    <div ref={ref} className={classNames} {...rest}>
      {headWrapper}
      {expanded && children[1]}
    </div>
  )
})

const DropdownClickWrapper = (props: DropdownBasePropsType & DropdownClickPropsType) => {
  const { expanded: propsExpaned, onExpand, onUnexpand, ...rest } = props;
  const [expanded, setExpanded] = React.useState(false);
  const dropDownRef = React.useRef<HTMLDivElement>(null);

  const handleExpand = React.useCallback<React.MouseEventHandler<HTMLDivElement>>(evt => {
    setExpanded(state => !state);
  }, [setExpanded]);

  const dropDownEl = dropDownRef.current;
  const handleUnexpand = React.useCallback((evt: MouseEvent) => {
    const { target } = evt;

    if (!(target instanceof Node)) {
      return;
    }

    if (dropDownEl?.contains(target)) {
      return;
    }

    setExpanded(false);
  }, [dropDownEl, setExpanded]);

  return (
    <Dropdown ref={dropDownRef}
      expanded={propsExpaned ?? expanded}
      onExpand={props.onExpand ?? handleExpand}
      onUnexpand={props.onUnexpand ?? handleUnexpand}
      {...rest}
    />
  )
};

const DropdownHoverWrapper = (props: DropdownBasePropsType & DropdownHoverPropsType) => {
  const { expanded: propsExpaned, onExpand, onUnexpand, ...rest } = props;
  const [expanded, setExpanded] = React.useState(false);
  const dropDownRef = React.useRef<HTMLDivElement>(null);

  const handleExpand = React.useCallback<React.MouseEventHandler<HTMLDivElement>>(evt => {
    setExpanded(true);
  }, [setExpanded]);

  const dropDownEl = dropDownRef.current;
  const handleUnexpand = React.useCallback((evt: MouseEvent) => {
    const { target } = evt;

    if (!(target instanceof Node)) {
      return;
    }

    if (dropDownEl === target || dropDownEl?.contains(target)) {
      return;
    }

    setExpanded(false);
  }, [dropDownEl, setExpanded]);

  return (
    <Dropdown ref={dropDownRef}
      expanded={propsExpaned ?? expanded}
      onExpand={props.onExpand ?? handleExpand}
      onUnexpand={props.onUnexpand ?? handleUnexpand}
      {...rest}
    />
  )
};

const DropdonwWrapper = React.memo(({
  activeAction = 'click',
  ...rest
}: DropdownPropsType) => {
  const Dropdown = (
    activeAction === 'click' ? DropdownClickWrapper : DropdownHoverWrapper
  ) as (props: DropdownPropsType) => JSX.Element;

  return (
    <Dropdown activeAction={activeAction} {...rest} />
  )
});

export default DropdonwWrapper;